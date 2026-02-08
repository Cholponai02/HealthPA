using System;
using System.Collections.Generic;
using System.IO;
using Camera.MAUI;
using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;

#if ANDROID
using Xamarin.TensorFlow.Lite;
#endif

namespace HealthPA.Views;

public partial class AItrackerPage : ContentPage
{
    private bool isProcessing = false;
#if ANDROID
    private Interpreter tflite;
#endif

    public AItrackerPage()
    {
        InitializeComponent();
        _ = LoadModelAsync(); // Запускаем загрузку модели в фоновом режиме
    }

    private async Task LoadModelAsync()
    {
        try
        {
#if ANDROID
            // 1. Читаем файл из ресурсов MAUI
            using var stream = await FileSystem.OpenAppPackageFileAsync("movenet_lightning.tflite");

            // 2. Копируем в массив байтов
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] modelBytes = memoryStream.ToArray();

            // 3. Создаем прямой Java ByteBuffer (это критично для работы TFLite на Android)
            var buffer = Java.Nio.ByteBuffer.AllocateDirect(modelBytes.Length);
            buffer.Order(Java.Nio.ByteOrder.NativeOrder());
            buffer.Put(modelBytes);

            // 4. Теперь передаем этот буфер в интерпретатор
            tflite = new Interpreter(buffer);

            System.Diagnostics.Debug.WriteLine("ИИ Модель успешно загружена через ByteBuffer");
#endif
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка: {ex.Message}");
        }
    }


    private bool isTracking = false;

    private async void OnStartClicked(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Camera>();

        if (status == PermissionStatus.Granted)
        {
            if (!isTracking)
            {
                // 1. Проверяем, загрузились ли камеры
                if (cameraView.Cameras.Count > 0)
                {
                    // Выбираем фронтальную камеру (для селфи-тренировки)
                    cameraView.Camera = cameraView.Cameras.FirstOrDefault(c => c.Position == CameraPosition.Front)
                                        ?? cameraView.Cameras.First();
                }

                // 2. Запускаем
                var result = await cameraView.StartCameraAsync();

                if (result == CameraResult.Success)
                {
                    isTracking = true;
                    _ = ProcessFramesLoop();
                    if (sender is Button btn) btn.Text = "Stop";
                    statusLabel.Text = "Pronti! (Готовы!)"; // Немного итальянского для практики
                }
                else
                {
                    await DisplayAlert("Ошибка", $"Камера не запустилась: {result}", "OK");
                }
            }
            else
            {
                await cameraView.StopCameraAsync();
                isTracking = false;
                if (sender is Button btn) btn.Text = "Start";
            }
        }
    }

    private async Task ProcessFramesLoop()
    {
        while (isTracking)
        {
            // Если ИИ еще занят прошлым кадром, просто ждем и пропускаем итерацию
            if (isProcessing)
            {
                await Task.Delay(100);
                continue;
            }

            try
            {
                // Используем GetSnapShot, как в вашем текущем коде
                var image = cameraView.GetSnapShot();
                if (image == null)
                {
                    System.Diagnostics.Debug.WriteLine("Snapshot вернул null!");
                    return;
                }
                if (image != null)
                {
#if ANDROID
                    isProcessing = true; // Поднимаем флаг: начали работу
                    AnalyzePose(image);
#endif
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка захвата: {ex.Message}");
            }

            // 500 мс (2 кадра в секунду) — оптимально для приседаний и не перегружает CPU
            await Task.Delay(500);
        }
    }

#if ANDROID
private void AnalyzePose(ImageSource source)
{
    Task.Run(async () =>
    {
        try
        {
            using var stream = await ((StreamImageSource)source).Stream(CancellationToken.None);
            using var bitmap = SKBitmap.Decode(stream);
            if (bitmap == null) return;
            System.Diagnostics.Debug.WriteLine($"Bitmap: {bitmap?.Width}x{bitmap?.Height}");

            // 1. Изменяем размер под вход модели (192x192)
            var resized = bitmap.Resize(new SKImageInfo(192, 192), SKFilterQuality.Low);

            // 2. Подготовка входного буфера (UINT8 - 1 байт на канал)
            var inputBuffer = Java.Nio.ByteBuffer.AllocateDirect(192 * 192 * 3);
            inputBuffer.Order(Java.Nio.ByteOrder.NativeOrder());
            inputBuffer.Rewind();

            for (int y = 0; y < 192; y++)
            {
                for (int x = 0; x < 192; x++)
                {
                    var color = resized.GetPixel(x, y);
                    // Java ByteBuffer ожидает sbyte (знаковый байт)
                    inputBuffer.Put((sbyte)color.Red);
                    inputBuffer.Put((sbyte)color.Green);
                    inputBuffer.Put((sbyte)color.Blue);
                }
            }

            // 3. Подготовка выходного массива строго формы [1, 1, 17, 3]
            // Это решает ошибку "Cannot copy... to [51]"
            var outputArray = new float[1][][][];
            outputArray[0] = new float[1][][];
            outputArray[0][0] = new float[17][];
            for (int i = 0; i < 17; i++)
            {
                outputArray[0][0][i] = new float[3];
            }

            // Оборачиваем многомерный массив в Java.Object
            var outputObj = Java.Lang.Object.FromArray(outputArray);

            // 4. Запуск модели
            tflite.Run(inputBuffer, outputObj);

            // 5. Преобразуем многомерный результат в плоский массив float[51] для логики счета
            float[] flatKeypoints = new float[51];
            for (int i = 0; i < 17; i++)
            {
                flatKeypoints[i * 3] = outputArray[0][0][i][0];     // Y (0..1)
                flatKeypoints[i * 3 + 1] = outputArray[0][0][i][1]; // X (0..1)
                flatKeypoints[i * 3 + 2] = outputArray[0][0][i][2]; // Confidence (Score)
            }

            // 6. Отправляем данные на отрисовку и в счетчик
            ProcessKeypoints(flatKeypoints);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка ИИ: {ex.Message}");
        }
        finally
        {
            isProcessing = false;
        }
    });
}
#endif

    private int squatCount = 0;
    private bool isDown = false;
    private float[] lastKeypoints; // Храним тут точки для отрисовки

    private void ProcessKeypoints(float[] keypoints)
    {
        System.Diagnostics.Debug.WriteLine($"Hip Y: {keypoints[33]}, Confidence: {keypoints[35]}");
        lastKeypoints = keypoints;
        // Координата Y бедра (индекс 11*3 = 33) и колена (индекс 13*3 = 39)
        // В MoveNet Y идет от 0 (верх) до 1 (низ экрана)
        float hipY = keypoints[33];
        float kneeY = keypoints[39];
        float confidence = keypoints[35]; // Уверенность модели в точке 11

        if (confidence < 0.3) return; // Игнорируем, если человека плохо видно

        // Если бедро опустилось достаточно низко (близко к колену или ниже)
        if (!isDown && hipY > (kneeY - 0.05f))
        {
            isDown = true;
        }
        // Если были внизу и поднялись обратно
        else if (isDown && hipY < (kneeY - 0.15f))
        {
            isDown = false;
            squatCount++;

            // Обновляем UI в основном потоке
            MainThread.BeginInvokeOnMainThread(() => {
                counterLabel.Text = $"Приседаний: {squatCount}";
                SemanticScreenReader.Announce($"Приседание {squatCount}"); // Для доступности
            });
        }
        // Заставляем Canvas перерисоваться
        MainThread.BeginInvokeOnMainThread(() => {
            canvasView?.InvalidateSurface();
        });
    }

    private void OnCanvasPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(); // Очищаем прошлый кадр

        if (lastKeypoints == null) return;

        var paint = new SKPaint { Color = SKColors.Lime, StrokeWidth = 5, IsAntialias = true };
        var circlePaint = new SKPaint { Color = SKColors.Red, Style = SKPaintStyle.Fill };

        // Проходим по 17 точкам (Y, X, Score)
        for (int i = 0; i < 17; i++)
        {
            float y = (float)(lastKeypoints[i * 3] * canvasView.Height);
            // Зеркалим X, если камера фронтальная
            float x = (float)((1 - lastKeypoints[i * 3 + 1]) * canvasView.Width);
            float score = lastKeypoints[i * 3 + 2];

            if (score > 0.3)
            {
                canvas.DrawCircle(x, y, 8, circlePaint);
            }
        }
    }

}