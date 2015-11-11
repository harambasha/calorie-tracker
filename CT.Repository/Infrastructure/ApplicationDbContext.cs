using System.Data.Entity;
using CT.Repository.Infrastructure.Identity;
using CT.Repository.Models.CalorieTracking;
using Microsoft.AspNet.Identity.EntityFramework;
using CT.Repository.Models.UserSetting;

namespace CT.Repository.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CalorieTrackingConnection", false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}