namespace HealthPA.Views;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage()
	{
		InitializeComponent();
	}

    private async void OnSportClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("SportPage");
    }
    private async void OnFoodClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("NutritionPage");
    }
    private async void OnAItrackerClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AItrackerPage");
    }

}