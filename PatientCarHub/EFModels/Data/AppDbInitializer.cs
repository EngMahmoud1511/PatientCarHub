using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using PatientCarHub.EFModels.Models;

namespace PatientCarHub.EFModels.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                await context.Database.EnsureCreatedAsync(); // Ensure database is created asynchronously

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                // Read admin details from appsettings.json
                var adminEmail = configuration["AdminUser:Email"];
                var adminPassword = configuration["AdminUser:Password"];
                var adminRole = configuration["AdminUser:Role"];

                // Check if the role exists, if not, create it
                var roleExists = await roleManager.FindByNameAsync(adminRole);
                if (roleExists == null)
                {
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(adminRole));
                    if (!createRoleResult.Succeeded)
                    {
                        throw new Exception($"Failed to create role '{adminRole}': {string.Join(", ", createRoleResult.Errors.Select(e => e.Description))}");
                    }
                }
                var roleExist = await roleManager.RoleExistsAsync("Patient");
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole("Patient"));
                }
                var rolerxist = await roleManager.RoleExistsAsync("Doctor");
                if (!rolerxist)
                {
                    await roleManager.CreateAsync(new IdentityRole("Doctor"));
                }

                // Check if the admin user exists
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    // Create the admin user
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (createUserResult.Succeeded)
                    {
                        // Assign the admin role to the user
                        var addToRoleResult = await userManager.AddToRoleAsync(adminUser, adminRole);
                        if (!addToRoleResult.Succeeded)
                        {
                            throw new Exception($"Failed to add user to role '{adminRole}': {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        throw new Exception($"Failed to create admin user: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }
}
