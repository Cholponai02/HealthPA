using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
namespace HealthPA.Views;

public partial class NutritionPage : ContentPage
{
    private readonly List<string> advices = new List<string>
        {
            "����� ���� �� 30 ����� �� ���, ����� �������� �����������.",
            "����� ������ ������ ��� ��������� ���������.",
            "�� ����������� �������, ��� ������ ����� ����.",
            "���������� ���� ��������, ����� ����� �������������� �������.",
            "���������� � ������ ������ ���������, ������� ������."
        };

    public NutritionPage()
    {
        InitializeComponent();
    }

    private void OnMoreAdviceClicked(object sender, EventArgs e)
    {
        // �������� ��������� �����
        var random = new Random();
        int index = random.Next(advices.Count);
        DailyAdviceLabel.Text = advices[index];
    }

    private async void OpenTracker(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new TrackerPage());
        await Shell.Current.GoToAsync("TrackerPage");
    }

    private async void OpenDietPlan(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("DietPlanPage");
    }

    private async void OpenArticles(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ArticlesPage");
    }

    private async void OnClaculateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CalculatorPage");
    }
}