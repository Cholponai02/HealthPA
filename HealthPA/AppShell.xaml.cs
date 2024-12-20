using HealthPA.Views;

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
        }
    }
}
