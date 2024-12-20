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

}