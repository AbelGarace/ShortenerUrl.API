using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShortenerUrl.Api.ApplicationCore.Enums;
using ShortenerUrl.Api.Infrastructure.Identity;

namespace ShortenerUrl.Api.Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager, int? retry = 0)
        {
            var retryForAvailability = retry.Value;
            try
            {
                context.Database.Migrate();

                await GetPreconfiguredUsers(context, userManager);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, userManager, retryForAvailability);
                }
            }
        }

        private static async Task GetPreconfiguredUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any(x => x.UserName.ToLower() == "usertest"))
            {
                var wunderDogUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString().ToUpper(),
                    UserName = "UserTest",
                    Email = "test@test.com",
                    FirstName = "User",
                    LastName = "Test"
                };
                await userManager.CreateAsync(wunderDogUser, "TestUser!14");
                await userManager.AddToRoleAsync(wunderDogUser, UserRolesEnum.User.ToString());
            }
        }

    }
}