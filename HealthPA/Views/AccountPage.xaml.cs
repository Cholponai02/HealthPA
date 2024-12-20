namespace HealthPA.Views;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}
    private async void OnMedicinesClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Лекарства", "Открыть раздел Лекарства", "OK");
    }

    private async void OnNutritionClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Питание", "Открыть раздел Питание", "OK");
    }

    private async void OnSleepClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Сон", "Открыть раздел Сон", "OK");
    }

    private async void OnMedicalRecordClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Медкарта", "Открыть раздел Медкарта", "OK");
    }
}