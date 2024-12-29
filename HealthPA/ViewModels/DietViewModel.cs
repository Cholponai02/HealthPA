using HealthPA.Models;
using HealthPA.Services;
using System.Windows.Input;

namespace HealthPA.ViewModels
{
    public class DietViewModel : BaseViewModel
    {
        private DietService _dietService;
        private List<DietGoal> _dietGoals;
        private List<Gender> _genders;
        private List<LifeStyle> _lifestyles;
        private List<Allergy> _allergies;
        private List<ProductRecommendation> _productRecommendations;
        private List<Menu> _weeklyMenu;
        public ICommand GenerateDietPlanCommand => new Command(async () => await GenerateDietPlan());

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<DietGoal> DietGoals
        {
            get => _dietGoals;
            set => SetProperty(ref _dietGoals, value);
        }

        public DietGoal SelectedGoal { get; set; }

        public List<Gender> Genders
        {
            get => _genders;
            set => SetProperty(ref _genders, value);
        }

        public Gender SelectedGender { get; set; }

        public List<LifeStyle> Lifestyles
        {
            get => _lifestyles;
            set => SetProperty(ref _lifestyles, value);
        }

        public LifeStyle SelectedLifestyle { get; set; }

        public List<Allergy> Allergies
        {
            get => _allergies;
            set => SetProperty(ref _allergies, value);
        }

        public List<ProductRecommendation> ProductRecommendations
        {
            get => _productRecommendations;
            set => SetProperty(ref _productRecommendations, value);
        }

        public List<Menu> WeeklyMenu
        {
            get => _weeklyMenu;
            set => SetProperty(ref _weeklyMenu, value);
        }

        // Конструктор, где мы инициализируем DietService
        public DietViewModel()
        {
            _dietService = new DietService(); // Инициализация сервиса
        }

        public async Task LoadDataAsync()
        {
            // Пример загрузки данных (здесь можно подставить реальные источники)
            DietGoals = await _dietService.GetDietGoalsAsync();
            Genders = new List<Gender> { new Gender { GenderUser = "Мужской" }, new Gender { GenderUser = "Женский" } };
            Lifestyles = new List<LifeStyle>
        {
            new LifeStyle { Lifestyle = "Сидячий" },
            new LifeStyle { Lifestyle = "Активный" },
            new LifeStyle { Lifestyle = "Спортивный" }
        };
            Allergies = await _dietService.GetAllergiesAsync();
            ProductRecommendations = await _dietService.GetProductRecommendationsAsync();
        }

        public async Task GenerateDietPlan()
        {
            IsLoading = true;
            if (SelectedGoal == null || SelectedGender == null || SelectedLifestyle == null)
            {
                // Проверка выбора
                return;
            }

            // Логика для расчета меню на неделю
            WeeklyMenu = await _dietService.GenerateWeeklyMenuAsync(SelectedGoal, SelectedGender, SelectedLifestyle, Allergies);
            IsLoading = false;
        }
    }

}
