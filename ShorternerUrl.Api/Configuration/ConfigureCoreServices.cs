using ShortenerUrl.Api.ApplicationCore.Interfaces;
using ShortenerUrl.Api.ApplicationCore.Services;

namespace ShorternerUrl.Api.Configuration
{
    public class ConfigureCoreServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ILinkService, LinkService>();
        }
    }
}
