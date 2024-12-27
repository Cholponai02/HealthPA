namespace HealthPA.Views.TrainingViews;
using HealthPA.ViewModels;


public partial class LegTrainingPage : ContentPage
{
    public LegTrainingPage()
    {
        InitializeComponent();
        BindingContext = new LegTrainingViewModel();
    }
    
}