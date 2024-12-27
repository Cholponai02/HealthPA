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

    private async void OnArmsTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ArmTrainingPage");
    }

    private async void OnLegsTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("LegTrainingPage");
    }

    private async void OnGlutesTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("GluteTrainingPage");
    }

    private async void OnAbsTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AbsTrainingPage");
    }

    private async void OnWarmUpTap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("WarmUpPage");
    }
}