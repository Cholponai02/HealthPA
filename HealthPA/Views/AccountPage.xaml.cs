namespace HealthPA.Views;

public partial class AccountPage : ContentPage
{
	public AccountPage()
	{
		InitializeComponent();
	}
    private async void OnMedicinesClicked(object sender, EventArgs e)
    {
        await DisplayAlert("���������", "������� ������ ���������", "OK");
    }

    private async void OnNutritionClicked(object sender, EventArgs e)
    {
        await DisplayAlert("�������", "������� ������ �������", "OK");
    }

    private async void OnSleepClicked(object sender, EventArgs e)
    {
        await DisplayAlert("���", "������� ������ ���", "OK");
    }

    private async void OnMedicalRecordClicked(object sender, EventArgs e)
    {
        await DisplayAlert("��������", "������� ������ ��������", "OK");
    }
}