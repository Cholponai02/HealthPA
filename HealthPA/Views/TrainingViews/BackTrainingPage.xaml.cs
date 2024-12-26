using HealthPA.ViewModels;
namespace HealthPA.Views.TrainingViews;

public partial class BackTrainingPage : ContentPage
{
	public BackTrainingPage()
	{
		InitializeComponent();
        BindingContext = new BackTrainingViewModel();
    }

    private async void StartTraining(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Тренировка для спины",
            "Вы хотите начать тренировку?", "Да", "Нет");

        if (confirm)
        {
            await DisplayAlert("Начало", "Тренировка началась!", "OK");

        }
    }
    private async void OnGifTapped(object sender, EventArgs e)
    {
        // Переход на страницу с анимацией GIF
        await Navigation.PushAsync(new GifAnimationPage("back1.gif"));
    }

}