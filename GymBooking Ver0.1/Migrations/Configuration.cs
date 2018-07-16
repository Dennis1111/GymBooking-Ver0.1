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
            var users = new[] { new ApplicationUser { UserName = "admin@gymbokning.se", Email = "admin@gymbokning.se", FirstName = "Admin", LastName = "Istrator", TimeOfRegistration = DateTime.Now},
                                new ApplicationUser { UserName = "dennis@gymbokning.se", Email = "dennis@gymbokning.se", FirstName = "Dennis", LastName = "Nilsson", TimeOfRegistration = DateTime.Now},
                                new ApplicationUser { UserName = "robert@gymbokning.se", Email = "robert@gmail.se", FirstName = "Robert", LastName = "Hansson", TimeOfRegistration = DateTime.Now}};

            var passwords = new[] { "AdMin1!", "passWord2!" };
            var emails = new[] { "admin@gymbokning.se"};
            int count = 0;

            foreach (var user in users)
            {
                Console.WriteLine("Seed user"+user.UserName);
                if (db.Users.Any(u => u.UserName == user.UserName)) continue;
                var result = userManager.Create(user, passwords[count]);
                if (!result.Succeeded)
                { throw new Exception(string.Join("\n", result.Errors)); }
                count++;
            }

            var adminUser = userManager.FindByName("admin@gymbokning.se");
            userManager.AddToRole(adminUser.Id, "Admin");

          
        }
    }
}
