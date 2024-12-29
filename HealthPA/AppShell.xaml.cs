using HealthPA.Views;
using HealthPA.Views.TrainingViews;
using HealthPA.Views.NutritionViews;

namespace HealthPA
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutingPages();
        }
        private void RegisterRoutingPages()
        {
            Routing.RegisterRoute("CalculatorPage", typeof(CalculatorPage));
            Routing.RegisterRoute("SportPage", typeof(SportPage));
            Routing.RegisterRoute("GifAnimationPage", typeof(GifAnimationPage));
            Routing.RegisterRoute("BackTrainingPage", typeof(BackTrainingPage));
            Routing.RegisterRoute("ArmTrainingPage", typeof(ArmTrainingPage));
            Routing.RegisterRoute("LegTrainingPage", typeof(LegTrainingPage));
            Routing.RegisterRoute("GluteTrainingPage", typeof(GluteTrainingPage));
            Routing.RegisterRoute("AbsTrainingPage", typeof(AbsTrainingPage));
            Routing.RegisterRoute("WarmUpPage", typeof(WarmUpPage));
            Routing.RegisterRoute("NutritionPage", typeof(NutritionPage));
            Routing.RegisterRoute("ArticlesPage", typeof(ArticlesPage));
            Routing.RegisterRoute("DietPlanPage", typeof(DietPlanPage));
            Routing.RegisterRoute("TrackerPage", typeof(TrackerPage));
        }
    }
}
