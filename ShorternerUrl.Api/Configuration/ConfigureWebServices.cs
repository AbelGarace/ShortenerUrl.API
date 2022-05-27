using ShorternerUrl.Api.Interfaces;
using ShorternerUrl.Api.Services;

namespace ShorternerUrl.Api.Configuration
{
    public class ConfigureWebServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ILinkDtoService, LinkDtoService>();           
        }
    }
}
