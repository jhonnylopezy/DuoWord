
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

namespace DuoWord.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,IConfigurationManager config,ILogger logger)
        {
            string? stringConex = config.GetConnectionString("ConnectionStrings.postgresql");
            Guard.Against.Null(stringConex);
            service.AddDbContext<DuoWordContext>(option =>
            option.UseNpgsql(stringConex));

            service.AddScoped<ICategoryRepository, CategoryRepository>();

            logger.LogInformation("Se registro la extension de la capa de infraestructura");

            return service; 
        }
    }
}
