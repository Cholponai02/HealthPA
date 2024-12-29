using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPA.Models
{
    public class Menu
    {
        public string Day { get; set; }
        public List<Meal> Meals { get; set; }  // Завтрак, Обед, Ужин
    }
}
