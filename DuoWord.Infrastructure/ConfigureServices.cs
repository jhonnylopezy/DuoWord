
using Ardalis.GuardClauses;
using DuoWord.Infrastructure.Data;
using DuoWord.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DuoWord.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, Assembly? callingAssembly = null)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Use for Specification Repository
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));
            // Use for Domaint Events
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(GetAssemblies(callingAssembly)));

            //services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddMemoryCache();
            services.AddScoped(typeof(ICommandBaseRepository<>), typeof(CommandRepository<>));
#if (EnableApiCore)
        var exampleApiCoreOptions = configuration.GetSection(nameof(ExampleApiCoreOptions)).Get<ExampleApiCoreOptions>();
        services.AddHttpClientPool<ExampleApiCoreHttpService>(exampleApiCoreOptions?.Url ?? throw new NotImplementedException("Api Core Url not configured"));
        services.AddSingleton<IExampleApiCoreService, ExampleApiCoreService>();
#endif
#if (EnableBusinessApi)
        var exampleBusinessOptions = configuration.GetSection(nameof(ExampleBusinessOptions)).Get<ExampleBusinessOptions>();
        services.AddHttpClientPool<ExampleBusinessHttpService>(exampleBusinessOptions?.Url ?? throw new NotImplementedException("Api Core Url not configured"));
        services.AddSingleton<IExampleBusinessServices, ExampleBusinessService>();
#endif
#if (EnableBtServices)
        //BtServices Section
        services.AddBtServices();
        services.AddScoped<IBTService, BTService>();
#endif
            return services;
        }

        private static Assembly[] GetAssemblies(Assembly? callingAssembly)
        {
            var _assemblies = new List<Assembly>();
            var coreAssembly =
              Assembly.GetAssembly(typeof(Bsol.ApiTemplate.Core.CoreAssembly));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }

            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
            return [.. _assemblies];
        }
    }
}
