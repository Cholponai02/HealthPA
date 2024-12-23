﻿using HealthPA.Views;
using HealthPA.Views.TrainingViews;

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
        }
    }
}
