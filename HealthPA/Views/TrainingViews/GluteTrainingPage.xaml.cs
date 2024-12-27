namespace HealthPA.Views.TrainingViews;
using HealthPA.ViewModels;

public partial class GluteTrainingPage : ContentPage
{
    public GluteTrainingPage()
    {
        InitializeComponent();
        BindingContext = new GluteTrainingViewModel();
    }
}