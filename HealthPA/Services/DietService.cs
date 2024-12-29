using HealthPA.Models;

namespace HealthPA.Services
{
    public class DietService
    {
        public async Task<List<Menu>> GenerateWeeklyMenuAsync(DietGoal goal, Gender gender, LifeStyle lifestyle, List<Allergy> allergies)
        {
            List<Menu> menu = new List<Menu>();

            // Загрузка всех доступных блюд
            var allMeals = await GetMealsAsync();

            Random random = new Random(); // Генератор случайных чисел

            for (int i = 0; i < 7; i++)  // Для каждого дня недели
            {
                var dailyMenu = new Menu
                {
                    Day = Enum.GetName(typeof(DayOfWeek), i),  // Понедельник, Вторник и т.д.
                    Meals = new List<Meal>()
                };

                // Генерация меню для каждого приема пищи
                dailyMenu.Meals.Add(GenerateMeal("Завтрак", allMeals, allergies, goal, gender, lifestyle, random));
                dailyMenu.Meals.Add(GenerateMeal("Обед", allMeals, allergies, goal, gender, lifestyle, random));
                dailyMenu.Meals.Add(GenerateMeal("Ужин", allMeals, allergies, goal, gender, lifestyle, random));

                menu.Add(dailyMenu);
            }

            return menu;
        }


        private Meal GenerateMeal(string mealType, List<Meal> allMeals, List<Allergy> allergies, DietGoal goal, Gender gender, LifeStyle lifestyle, Random random)
        {
            // Фильтрация блюд
            var filteredMeals = allMeals.Where(m => !HasAllergy(m, allergies) && IsValidForGoal(m, goal, gender, lifestyle)).ToList();

            if (filteredMeals.Count == 0)
                throw new InvalidOperationException("Нет доступных блюд после фильтрации!");

            // Выбор случайного блюда
            var selectedMeal = filteredMeals[random.Next(filteredMeals.Count)];
            return selectedMeal;
        }


        private bool HasAllergy(Meal meal, List<Allergy> allergies)
        {
            return allergies.Any(a => meal.Description.Contains(a.Allergen));
        }

        private bool IsValidForGoal(Meal meal, DietGoal goal, Gender gender, LifeStyle lifestyle)
        {
            if (goal.Goal == "Похудение" && meal.Calories > 500) return false;
            if (goal.Goal == "Набор массы" && meal.Calories < 400) return false;

            return true;
        }

        public async Task<List<Meal>> GetMealsAsync()
        {
            // Загрузка всех доступных блюд. Замените на реальную логику
            return new List<Meal>
    {
        // Завтрак
        new Meal { Name = "Овсяная каша", Description = "Овсянка с ягодами и медом", Calories = 250 },
        new Meal { Name = "Омлет", Description = "Омлет с овощами", Calories = 300 },
        new Meal { Name = "Тост с авокадо", Description = "Цельнозерновой хлеб с авокадо", Calories = 280 },
        new Meal { Name = "Йогурт с гранолой", Description = "Натуральный йогурт с гранолой и фруктами", Calories = 200 },

        // Обед
        new Meal { Name = "Курица с рисом", Description = "Курица с овощным рисом", Calories = 600 },
        new Meal { Name = "Рыба с картофелем", Description = "Жареная рыба с картофельным пюре", Calories = 400 },
        new Meal { Name = "Суп из чечевицы", Description = "Питательный суп из чечевицы", Calories = 350 },
        new Meal { Name = "Салат Цезарь", Description = "Салат с курицей и сыром Пармезан", Calories = 450 },

        // Ужин
        new Meal { Name = "Тушеные овощи с киноа", Description = "Овощи, тушеные с киноа", Calories = 300 },
        new Meal { Name = "Гречка с грибами", Description = "Гречневая каша с грибами", Calories = 350 },
        new Meal { Name = "Куриная грудка на пару", Description = "Диетическая куриная грудка с зеленью", Calories = 400 },
        new Meal { Name = "Тунец с овощами", Description = "Тунец с овощным гарниром", Calories = 320 },

        // Перекусы
        new Meal { Name = "Орехи и сухофрукты", Description = "Микс из орехов и сухофруктов", Calories = 200 },
        new Meal { Name = "Фрукты", Description = "Свежие фрукты (яблоки, бананы, груши)", Calories = 150 },
        new Meal { Name = "Смузи", Description = "Фруктово-ягодный смузи", Calories = 180 },
        new Meal { Name = "Протеиновый батончик", Description = "Полезный батончик для перекуса", Calories = 220 }
    };
        }


        public async Task<List<DietGoal>> GetDietGoalsAsync()
        {
            // Пример получения целей
            return new List<DietGoal>
        {
            new DietGoal { Goal = "Похудение", Description = "Снижение массы тела" },
            new DietGoal { Goal = "Набор массы", Description = "Увеличение массы тела" },
            new DietGoal { Goal = "Поддержание веса", Description = "Сохранение текущего веса" }
        };
        }

        public async Task<List<Allergy>> GetAllergiesAsync()
        {
            // Пример получения аллергенов
            return new List<Allergy>
        {
            new Allergy { Allergen = "Лактоза", IsSelected = false },
            new Allergy { Allergen = "Глютен", IsSelected = false }
        };
        }

        public async Task<List<ProductRecommendation>> GetProductRecommendationsAsync()
        {
            // Пример получения рекомендаций по продуктам
            return new List<ProductRecommendation>
        {
            new ProductRecommendation { Name = "Овсянка", Category = "Завтрак" },
            new ProductRecommendation { Name = "Курица", Category = "Белок" },
            new ProductRecommendation { Name = "Брокколи", Category = "Овощи" }
        };
        }
    }

}
