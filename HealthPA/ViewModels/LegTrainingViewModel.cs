using System.Collections.ObjectModel;
using HealthPA.Models;

namespace HealthPA.ViewModels
{
    public class LegTrainingViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public LegTrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "leg1.gif",
                    Title = "1. Приседания с гантелями",
                    Description = "Встаньте, держа гантели в руках, приседайте до параллели бедер с полом и возвращайтесь в исходное положение."
                },
                new Exercise
                {
                    ImageSource = "leg2.jpg",
                    Title = "2. Выпады вперед",
                    Description = "Шагайте вперед одной ногой и опускайтесь до угла 90 градусов в колене, затем возвращайтесь в исходное положение."
                },
                new Exercise
                {
                    ImageSource = "leg3.jpg",
                    Title = "3. Становая тяга",
                    Description = "Согните колени и наклонитесь вперед, держите гантели в руках, поднимайтесь, выпрямляя спину."
                }
            };
        }
    }

}
