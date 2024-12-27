using HealthPA.ViewModels;

namespace HealthPA.Views.TrainingViews;

public partial class AbsTrainingPage : ContentPage
{
    public AbsTrainingPage()
    {
        InitializeComponent();
        BindingContext = new AbsTrainingViewModel();
    }
}