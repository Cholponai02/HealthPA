using HealthPA.Models;
using System.Collections.ObjectModel;

namespace HealthPA.ViewModels
{
    public class WarmUpViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public WarmUpViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "warmup1.gif",
                    Title = "1. Круговые вращения плечами",
                    Description = "Встаньте прямо, сделайте вращения плечами вперед и назад по 10 раз в каждую сторону."
                },
                new Exercise
                {
                    ImageSource = "warmup2.jpg",
                    Title = "2. Наклоны головы",
                    Description = "Наклоните голову вперед, затем назад, делайте наклоны влево и вправо по 10 раз."
                },
                new Exercise
                {
                    ImageSource = "warmup3.jpg",
                    Title = "3. Вращения тазом",
                    Description = "Встаньте прямо, поставьте ноги на ширине плеч, сделайте вращения тазом в одну и другую сторону."
                }
            };
        }
    }

}
