namespace DuoWord.Presentation.Configurations
{
    public static class MediatrConfigs
    { 
        public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return services;
        }


    }
}
