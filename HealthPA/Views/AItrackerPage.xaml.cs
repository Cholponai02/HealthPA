using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Controls;

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

}