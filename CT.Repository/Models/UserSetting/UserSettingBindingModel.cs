using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Repository.Models.UserSetting
{
    public class UserSettingBindingModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Calories consumed cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1} for calories consumed")]
        public int DailyCalories { get; set; }


        public UserSetting CreateUserSetting()
        {
            return new UserSetting()
            {
                UserId = this.UserId,
                DailyCalories = this.DailyCalories
            };
        }

        public UserSetting UpdateModel(UserSetting userSetting)
        {
            userSetting.DailyCalories = this.DailyCalories;
            return userSetting;
        }
    }
}
