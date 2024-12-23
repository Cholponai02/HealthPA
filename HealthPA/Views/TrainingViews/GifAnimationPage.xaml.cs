namespace HealthPA.Views.TrainingViews;

public partial class GifAnimationPage : ContentPage
{
	public GifAnimationPage()
	{
		InitializeComponent();
	}

    public GifAnimationPage(string gifSource)
    {
        InitializeComponent();
        //GifImage.Source = gifSource; // Установите GIF как источник изображения
    }
}