using HealthPA.ViewModels;

namespace HealthPA.Views.TrainingViews;

public partial class WarmUpPage : ContentPage
{
    public WarmUpPage()
    {
        InitializeComponent();
        BindingContext = new WarmUpViewModel();
    }
}