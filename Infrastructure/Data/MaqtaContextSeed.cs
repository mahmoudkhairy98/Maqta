using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MaqtaContextSeed
    {
        public static async Task SeedAsync(MaqtaContext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager)
        {
            try
            {
                if (!context.Employees.Any())
                {
                    var employeesData = File.ReadAllText("../Infrastructure/Data/SeedData/employees.json");
                    var employees = JsonSerializer.Deserialize<List<Employee>>(employeesData);
                    foreach (var item in employees)
                    {
                        context.Employees.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        DisplayName = "Maqta",
                        Email = "maqta@test.com",
                        UserName = "maqta@test.com"
                    };

                    await userManager.CreateAsync(user, "P@ssw0rd");
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<MaqtaContextSeed>();
                logger.LogError(ex.Message, "An error occured during seeding data");
            }
        }
    }
}
