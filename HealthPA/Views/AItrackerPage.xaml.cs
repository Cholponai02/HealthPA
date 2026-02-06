using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;
using SkiaSharp;

#if ANDROID
using Xamarin.TensorFlow.Lite;
#endif

namespace HealthPA.Views;

public partial class AItrackerPage : ContentPage
{
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
                await cameraView.StartCameraAsync();
                isTracking = true;

                // Запускаем бесконечный цикл обработки кадров, пока isTracking == true
                _ = ProcessFramesLoop();

                if (sender is Button btn) btn.Text = "Stop";
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
            try
            {
                // Пытаемся захватить кадр. 
                // Попробуй cameraView.GetSnapshot() или cameraView.TakeSnapshot()
                // Если названия другие, начни писать cameraView.Take... и VS подскажет
                var image = cameraView.GetSnapShot();

                if (image != null)
                {
                    // Отправляем на анализ в твой TensorFlow
                    // ProcessImage(image); 
                }
            }
            catch { /* Игнорируем ошибки захвата */ }

            // Ждем 200 мс (это даст нам ~5 кадров в секунду)
            await Task.Delay(200);
        }
    }

}