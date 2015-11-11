using System.Collections.Generic;
using CT.Repository.Infrastructure;
using CT.Repository.Models.CalorieTracking;
using System;

namespace CT.Repository.Repository.Meals
{
    public interface IMealRepository : IGenericRepository<Meal>
    {
        List<Meal> GetMealsByUserId(string userId);
        List<Meal> GetMealsByUserIdFilteredByDateAndTime(string userId, DateTime startDate, DateTime endDate, DateTime fromTime, DateTime toTime);
    }
}
