namespace HealthPA.Views.NutritionViews;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
	}

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        // Получаем данные из полей
        string sex = sexEntry.Text?.ToLower() ?? "ж";
        double weight = Convert.ToDouble(weightEntry.Text);
        double height = Convert.ToDouble(heightEntry.Text);
        double age = Convert.ToDouble(ageEntry.Text);
        string activity = activityPicker.SelectedItem?.ToString() ?? "Сидячий";

        double bmrMan = 88.36 + (13.4 * weight) + (4.8 * height) - (5.7 * age);
        double bmrWoman = 447.593 + (9.247 * weight) + (3.098 * height) - (4.33 * age);
        double dailyCalories = 0;

        // Определяем множитель для активности
        double activityFactor = activity switch
        {
            "Сидячий" => 1.2,
            "Тренировки 1-3 раза в неделю" => 1.375,
            "Занятия 3-5 дней в неделю" => 1.55,
            "Интенсивные тренировки 6-7 раз в неделю" => 1.725,
            "Спортсмены, упражнения чаще чем раз в день" => 1.9,
            _ => 1.2
        };

        // Вычисление суточной нормы калорий в зависимости от пола
        if (sex == "м")
        {
            dailyCalories = bmrMan * activityFactor;
        }
        else if (sex == "ж")
        {
            dailyCalories = bmrWoman * activityFactor;
        }

        // Расчеты для белков, жиров и углеводов
        double protein = weight * 1.5;
        double fat = weight * 0.8;
        double carbs = weight * 2;

        // Отображаем результаты
        resultLayout.IsVisible = true;
        caloriesResult.Text = $"Суточная норма калорий: {dailyCalories} ккал";
        proteinResult.Text = $"Суточная норма белка: {protein} г";
        fatResult.Text = $"Суточная норма жиров: {fat} г";
        carbsResult.Text = $"Суточная норма углеводов: {carbs} г";
    }

}