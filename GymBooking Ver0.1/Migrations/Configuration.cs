namespace GymBooking_Ver0._1.Migrations
{
    using GymBooking_Ver0._1.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Admin"};
            foreach (var roleName in roleNames)
            {
                if (db.Roles.Any(r => r.Name == roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var emails = new[] { "admin@gymbokning.se"};

            foreach (var email in emails)
            {
                if (db.Users.Any(u => u.UserName == email)) continue;
                var user = new ApplicationUser { UserName = email, Email = email , FirstName = "Dennis",LastName = "Nilsson",TimeOfRegistration= DateTime.Now};
                var result = userManager.Create(user, "password");
                if (!result.Succeeded)
                { throw new Exception(string.Join("\n", result.Errors)); }
            }

            var adminUser = userManager.FindByName("admin@gymbokning.se");
            userManager.AddToRole(adminUser.Id, "Admin");          

        }
    }
}
