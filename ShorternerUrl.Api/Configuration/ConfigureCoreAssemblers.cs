using ShortenerUrl.Api.ApplicationCore.DtoAssemblers;
using ShortenerUrl.Api.ApplicationCore.Interfaces;

namespace ShorternerUrl.Api.Configuration
{
    public class ConfigureCoreAssemblers
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ILinkAssembler, LinkAssembler>();
        }
    }
}
