using System.Collections.ObjectModel;
using HealthPA.Models;
namespace HealthPA.ViewModels
{
    public class GluteTrainingViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public GluteTrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "glute1.gif",
                    Title = "1. Подъем таза (мостик)",
                    Description = "Лягте на спину, согните колени, стопы на полу. Поднимайте таз вверх, удерживайте в верхней точке 2-3 секунды."
                },
                new Exercise
                {
                    ImageSource = "glute2.jpg",
                    Title = "2. Выпады назад",
                    Description = "Шагайте одной ногой назад и опускайтесь до угла 90 градусов в колене, затем возвращайтесь в исходное положение."
                },
                new Exercise
                {
                    ImageSource = "glute3.jpg",
                    Title = "3. Отведение ноги в сторону",
                    Description = "Встаньте на колени и руки, отводите одну ногу в сторону, удерживайте в верхней точке 1-2 секунды."
                }
            };
        }
    }

}
