using System.Collections.ObjectModel;
using HealthPA.Models;

namespace HealthPA.ViewModels
{
    public class ArmTrainingViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }

        public ArmTrainingViewModel()
        {
            Exercises = new ObservableCollection<Exercise>
            {
                new Exercise
                {
                    ImageSource = "arm1.gif",
                    Title = "1. Отжимания от пола",
                    Description = "Примите упор лежа, руки на ширине плеч, отжимайтесь, пока грудь не коснется пола."
                },
                new Exercise
                {
                    ImageSource = "arm2.jpg",
                    Title = "2. Сгибания рук с гантелями",
                    Description = "Встаньте, держите гантели в руках, сгибайте локти, поднимая гантели к плечам."
                },
                new Exercise
                {
                    ImageSource = "arm3.jpg",
                    Title = "3. Трицепс на брусьях",
                    Description = "Поднимитесь на брусья, опускайтесь вниз, сгибая локти, затем выжмите себя обратно."
                }
            };
        }
    }
}
