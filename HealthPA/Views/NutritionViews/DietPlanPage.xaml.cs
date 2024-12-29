using HealthPA.ViewModels;
namespace HealthPA.Views.NutritionViews;

public partial class DietPlanPage : ContentPage
{
    public DietPlanPage()
    {
        InitializeComponent();
        BindingContext = new DietViewModel();
    }

    private async void OnHelpClicked(object sender, EventArgs e)
    {
        await DisplayAlert("���� �������", "���� ������� ������� ��� ������� ����� � ����������� �� ����� ������������.", "OK");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as DietViewModel;
        await viewModel.LoadDataAsync();
    }

    //private async void OnCreateDietPlanClicked(object sender, EventArgs e)
    //{
    //    var viewModel = BindingContext as DietViewModel;
    //    await viewModel.GenerateDietPlan();
    //}

}