using System.Collections.ObjectModel;
using HealthPA.Models;

namespace HealthPA.ViewModels
{
    public class AbsTrainingViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public AbsTrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "abs1.gif",
                    Title = "1. Скручивания",
                    Description = "Лягте на спину, согните колени. Поднимайте верхнюю часть тела, скручиваясь, не отрывая поясницу от пола."
                },
                new Exercise
                {
                    ImageSource = "abs2.jpg",
                    Title = "2. Планка на локтях",
                    Description = "Примите положение на локтях и носках, держите спину ровной и не прогибайтесь в пояснице."
                },
                new Exercise
                {
                    ImageSource = "abs3.jpg",
                    Title = "3. Подъемы ног",
                    Description = "Лягте на спину, поднимайте ноги вверх, удерживайте в верхней точке 1-2 секунды, не касаясь пола."
                }
            };
        }
    }

}
