using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.Infrastructure.Data;

namespace ShorternerUrl.Api.Configuration
{
    public class ConfigureInfrastructureServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
