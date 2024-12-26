namespace HealthPA.Views.TrainingViews;
using HealthPA.ViewModels;

public partial class ArmTrainingPage : ContentPage
{
    public ArmTrainingPage()
    {
        InitializeComponent();
        BindingContext = new ArmTrainingViewModel();
    }
}