namespace HealthPA.Views;

public partial class SportPage : ContentPage
{
	public SportPage()
	{
		InitializeComponent();
	}

    private void OnSpineTap(object sender, EventArgs e)
    {
        DisplayAlert("���������� ��� �����", "�� ������� ���������� ��� �����.", "OK");
    }

    private void OnArmsTap(object sender, EventArgs e)
    {
        DisplayAlert("���������� ��� ���", "�� ������� ���������� ��� ���.", "OK");
    }

    private void OnLegsTap(object sender, EventArgs e)
    {
        DisplayAlert("���������� ��� ���", "�� ������� ���������� ��� ���.", "OK");
    }

    private void OnGlutesTap(object sender, EventArgs e)
    {
        DisplayAlert("���������� ��� ������", "�� ������� ���������� ��� ������.", "OK");
    }

    private void OnAbsTap(object sender, EventArgs e)
    {
        DisplayAlert("���������� ��� ������", "�� ������� ���������� ��� ������.", "OK");
    }

    private void OnWarmUpTap(object sender, EventArgs e)
    {
        DisplayAlert("��������", "�� ������� ��������.", "OK");
    }
}