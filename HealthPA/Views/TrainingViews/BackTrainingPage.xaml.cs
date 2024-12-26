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
        bool confirm = await DisplayAlert("���������� ��� �����",
            "�� ������ ������ ����������?", "��", "���");

        if (confirm)
        {
            await DisplayAlert("������", "���������� ��������!", "OK");

        }
    }
    private async void OnGifTapped(object sender, EventArgs e)
    {
        // ������� �� �������� � ��������� GIF
        await Navigation.PushAsync(new GifAnimationPage("back1.gif"));
    }

}