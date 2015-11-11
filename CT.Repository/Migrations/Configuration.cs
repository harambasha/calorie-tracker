using System;
using System.Data.Entity.Migrations;
using System.Linq;
using CT.Repository.Infrastructure;
using CT.Repository.Infrastructure.Identity;
using CT.Repository.Models.CalorieTracking;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CT.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "SuperPowerUser",
                Email = "ismirharambasic@gmail.com",
                EmailConfirmed = true,
                FirstName = "Ismir",
                LastName = "Harambasic",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "Password11");
            var adminUser = manager.FindByName("SuperPowerUser");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "UserManager" });
                roleManager.Create(new IdentityRole { Name = "Admin"});
                roleManager.Create(new IdentityRole { Name = "User"});
            }
            manager.AddToRoles(adminUser.Id, "UserManager", "Admin");

            var user2 = new ApplicationUser()
            {
                UserName = "UserManagement",
                Email = "ismirharambasic@gmail.com",
                EmailConfirmed = true,
                FirstName = "Ismir",
                LastName = "Harambasic",
                Level = 1,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user2, "Password22");
            var adminUser2 = manager.FindByName("UserManagement");
            manager.AddToRoles(adminUser2.Id, "UserManager");

            context.Meals.AddOrUpdate(new Meal() { UserId = "c1fe5d64-2ebc-4f2b-989d-ff8b615c43ad", CaloriesConsumed = 200, Date = DateTime.Now, Time = TimeSpan.Zero, Description = "Some description" });

        }
    }
}
