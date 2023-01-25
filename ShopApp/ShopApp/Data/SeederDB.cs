using Microsoft.AspNetCore.Identity;
using ShopApp.Constants;
using ShopApp.Data.Entities.Identity;

namespace ShopApp.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

               
                if (!managerRole.Roles.Any())
                {
                    var result = managerRole.CreateAsync(new AppRole
                    {
                        Name = Roles.Admin
                    }).Result;

                    result = managerRole.CreateAsync(new AppRole
                    {
                        Name = Roles.User
                    }).Result;
                }
                if (!manager.Users.Any())
                {
                    string email = "admin@gmail.com";
                    var user = new AppUser
                    {
                        Email = email,
                        UserName = email,
                        Photo = "fgbugdqn.bdv.jpeg",
                        PhoneNumber = "+11(111)111-11-11"
                    };
                    var result = manager.CreateAsync(user, "Qwerty1+").Result;
                    result = manager.AddToRoleAsync(user, Roles.Admin).Result;
                }
            }
        }
    }
}
