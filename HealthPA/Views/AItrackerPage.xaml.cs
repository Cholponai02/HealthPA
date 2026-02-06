using System;
using System.Collections.Generic;
using System.IO;
using Camera.MAUI;
using Microsoft.Maui.Controls;
using SkiaSharp;

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
                // 1. Конвертируем ImageSource в поток
                using var stream = await ((StreamImageSource)source).Stream(CancellationToken.None);
                using var bitmap = SKBitmap.Decode(stream);
                if (bitmap == null) return;
                // 2. Ресайз до 192x192 (требование MoveNet)
                var resized = bitmap.Resize(new SKImageInfo(192, 192), SKFilterQuality.Low);

                // 3. Создаем входной буфер для модели
                var inputBuffer = Java.Nio.ByteBuffer.AllocateDirect(192 * 192 * 3 * 4);
                inputBuffer.Order(Java.Nio.ByteOrder.NativeOrder());

                // Заполняем буфер пикселями
                for (int y = 0; y < 192; y++)
                {
                    for (int x = 0; x < 192; x++)
                    {
                        var color = resized.GetPixel(x, y);
                        inputBuffer.PutFloat(color.Red / 255.0f);
                        inputBuffer.PutFloat(color.Green / 255.0f);
                        inputBuffer.PutFloat(color.Blue / 255.0f);
                    }
                }

                // 4. Готовим выходной массив [1, 1, 17, 3]
                // 17 ключевых точек (глаза, плечи, колени...), у каждой: Y, X, Score
                var output = new float[1 * 1 * 17 * 3];
                var outputObj = Java.Lang.Object.FromArray(output);

                // 5. Запуск ИИ
                tflite.Run(inputBuffer, outputObj);

                // 6. Считаем приседание
                ProcessKeypoints(output);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка ИИ: {ex.Message}");
            }
            finally
            {
                isProcessing = false; // Опускаем флаг: теперь мы готовы к новому кадру
            }
        });
    }
#endif

    private int squatCount = 0;
    private bool isDown = false;

    private void ProcessKeypoints(float[] keypoints)
    {
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
    }

}