using System.Collections.ObjectModel;
using HealthPA.Models;

namespace HealthPA.ViewModels
{
    public class BackTrainingViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public BackTrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "back1.gif",
                    Title = "1. Подъемы корпуса (супермен)",
                    Description = "Лягте на живот, вытяните руки вперед. Поднимайте одновременно руки и ноги, удерживаясь в верхней точке 2-3 секунды."
                },
                new Exercise
                {
                    ImageSource = "exercise2.jpg",
                    Title = "2. Планка с подъемом руки",
                    Description = "Примите упор лежа, вытяните одну руку вперед, удерживайте равновесие 10-15 секунд, затем смените руку."
                },
                new Exercise
                {
                    ImageSource = "exercise3.jpg",
                    Title = "3. Разведения гантелей в наклоне",
                    Description = "Встаньте, наклоните корпус вперед, разведите руки с гантелями в стороны. Повторите 12-15 раз."
                }
            };
        }
    }

    
}
