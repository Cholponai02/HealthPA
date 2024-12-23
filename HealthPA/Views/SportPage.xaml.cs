namespace HealthPA.Views;

public partial class SportPage : ContentPage
{
	public SportPage()
	{
		InitializeComponent();
	}
    
    private async void OnSpineTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("BackTrainingPage");
    }

    private void OnArmsTap(object sender, EventArgs e)
    {
        DisplayAlert("Тренировка для рук", "Вы выбрали тренировку для рук.", "OK");
    }

    private void OnLegsTap(object sender, EventArgs e)
    {
        DisplayAlert("Тренировка для ног", "Вы выбрали тренировку для ног.", "OK");
    }

    private void OnGlutesTap(object sender, EventArgs e)
    {
        DisplayAlert("Тренировка для ягодиц", "Вы выбрали тренировку для ягодиц.", "OK");
    }

    private void OnAbsTap(object sender, EventArgs e)
    {
        DisplayAlert("Тренировка для пресса", "Вы выбрали тренировку для пресса.", "OK");
    }

    private void OnWarmUpTap(object sender, EventArgs e)
    {
        DisplayAlert("Разминка", "Вы выбрали разминку.", "OK");
    }
}