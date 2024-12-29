namespace HealthPA.Views.NutritionViews;

public partial class TrackerPage : ContentPage
{
    // ������� ��������� � �� ������������ �� 100 �����
    private readonly Dictionary<string, double> _caloriesDictionary = new Dictionary<string, double>
        {
            { "������ ����", 265 },
            { "������", 47 },
            { "�����", 89 },
            { "������� ����", 165 },
            { "����", 155 },
            { "���", 130 },
        };

    public TrackerPage()
    {
        InitializeComponent();
    }

    // ����� ��� ��������� ����� �� ������
    private void OnCalculateCaloriesClicked(object sender, EventArgs e)
    {
        var product = ProductEntry.Text?.ToLower(); // ����� ��������
        var quantityText = QuantityEntry.Text; // ���������� � �������

        if (string.IsNullOrEmpty(product) || string.IsNullOrEmpty(quantityText) || !double.TryParse(quantityText, out var quantity))
        {
            CaloriesLabel.Text = "����������, ������� ���������� ������!";
            return;
        }

        // �������� �� ������� �������� � �������
        if (_caloriesDictionary.TryGetValue(product, out var caloriesPer100g))
        {
            // ������ �������
            var totalCalories = (caloriesPer100g * quantity) / 100;
            CaloriesLabel.Text = $"�������: {totalCalories:F2} ����";
        }
        else
        {
            CaloriesLabel.Text = "���� ������� �� ������ � ����.";
        }
    }
}