using DuoWord.Infrastructure;

namespace DuoWord.Presentation.Configurations
{
    public static class ServiceConfigs
    {
        public static IServiceCollection AddServiceConfigs(this IServiceCollection service,IConfigurationManager config,ILogger logger,WebApplicationBuilder builder)
        {
            service.AddInfrastructureServices(config,logger)
                .AddMediatrConfigs();
            return service;
        }
    }
}
