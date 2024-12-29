namespace HealthPA.Views.NutritionViews;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
	}

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        // �������� ������ �� �����
        string sex = sexEntry.Text?.ToLower() ?? "�";
        double weight = Convert.ToDouble(weightEntry.Text);
        double height = Convert.ToDouble(heightEntry.Text);
        double age = Convert.ToDouble(ageEntry.Text);
        string activity = activityPicker.SelectedItem?.ToString() ?? "�������";

        double bmrMan = 88.36 + (13.4 * weight) + (4.8 * height) - (5.7 * age);
        double bmrWoman = 447.593 + (9.247 * weight) + (3.098 * height) - (4.33 * age);
        double dailyCalories = 0;

        // ���������� ��������� ��� ����������
        double activityFactor = activity switch
        {
            "�������" => 1.2,
            "���������� 1-3 ���� � ������" => 1.375,
            "������� 3-5 ���� � ������" => 1.55,
            "����������� ���������� 6-7 ��� � ������" => 1.725,
            "����������, ���������� ���� ��� ��� � ����" => 1.9,
            _ => 1.2
        };

        // ���������� �������� ����� ������� � ����������� �� ����
        if (sex == "�")
        {
            dailyCalories = bmrMan * activityFactor;
        }
        else if (sex == "�")
        {
            dailyCalories = bmrWoman * activityFactor;
        }

        // ������� ��� ������, ����� � ���������
        double protein = weight * 1.5;
        double fat = weight * 0.8;
        double carbs = weight * 2;

        // ���������� ����������
        resultLayout.IsVisible = true;
        caloriesResult.Text = $"�������� ����� �������: {dailyCalories} ����";
        proteinResult.Text = $"�������� ����� �����: {protein} �";
        fatResult.Text = $"�������� ����� �����: {fat} �";
        carbsResult.Text = $"�������� ����� ���������: {carbs} �";
    }

}