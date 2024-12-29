namespace HealthPA.Views.NutritionViews;

public partial class TrackerPage : ContentPage
{
    // Словарь продуктов и их калорийности на 100 грамм
    private readonly Dictionary<string, double> _caloriesDictionary = new Dictionary<string, double>
        {
            { "ржаной хлеб", 265 },
            { "яблоко", 47 },
            { "банан", 89 },
            { "куриное филе", 165 },
            { "яйцо", 155 },
            { "рис", 130 },
        };

    public TrackerPage()
    {
        InitializeComponent();
    }

    // Метод для обработки клика по кнопке
    private void OnCalculateCaloriesClicked(object sender, EventArgs e)
    {
        var product = ProductEntry.Text?.ToLower(); // Текст продукта
        var quantityText = QuantityEntry.Text; // Количество в граммах

        if (string.IsNullOrEmpty(product) || string.IsNullOrEmpty(quantityText) || !double.TryParse(quantityText, out var quantity))
        {
            CaloriesLabel.Text = "Пожалуйста, введите корректные данные!";
            return;
        }

        // Проверка на наличие продукта в словаре
        if (_caloriesDictionary.TryGetValue(product, out var caloriesPer100g))
        {
            // Расчет калорий
            var totalCalories = (caloriesPer100g * quantity) / 100;
            CaloriesLabel.Text = $"Калории: {totalCalories:F2} ккал";
        }
        else
        {
            CaloriesLabel.Text = "Этот продукт не найден в базе.";
        }
    }
}