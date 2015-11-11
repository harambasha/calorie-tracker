using CT.Repository.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Repository.Models.CalorieTracking
{
    public class MealBindingModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Calories Consumed")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1} for calories consumed")]
        public int CaloriesConsumed { get; set; }

        public Meal CreateMeal()
        {
            return new Meal()
            {
                UserId = this.UserId,
                Description = this.Description,
                Date = this.Date,
                Time = this.Time.TimeOfDay,
                CaloriesConsumed = this.CaloriesConsumed
            };
        }

        public Meal UpdateModel(Meal retrievedMeal)
        {
            retrievedMeal.Description = this.Description;
            retrievedMeal.Date = this.Date;
            retrievedMeal.Time = this.Time.TimeOfDay;
            retrievedMeal.CaloriesConsumed = this.CaloriesConsumed;
            return retrievedMeal;
        }
    }
}
