using System.Collections.Generic;
using System.Linq;
using CT.Repository.Infrastructure;
using CT.Repository.Models.CalorieTracking;
using System;

namespace CT.Repository.Repository.Meals
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(ApplicationDbContext dataContext)
            : base(dataContext)
        {
        }

        public List<Meal> GetMealsByUserId(string userId)
        {
            return DataSource().Where(m => m.UserId == userId).ToList();
        }

        public List<Meal> GetMealsByUserIdFilteredByDateAndTime(string userId, DateTime startDate, DateTime endDate, DateTime fromTime, DateTime toTime)
        {
            TimeSpan fromTimeTimespan = fromTime.TimeOfDay;
            TimeSpan toTimeSpan = toTime.TimeOfDay;

            return DataSource().Where(m => m.UserId == userId &&
                                      m.Date >= startDate &&
                                      m.Date <= endDate &&
                                      m.Time >= fromTimeTimespan &&
                                      m.Time <= toTimeSpan).ToList();
        }
    }
}
