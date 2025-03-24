using DuoWord.Domain.Entities;
using DuoWord.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace DuoWord.Infrastructure
{
    public static class StartupSetup
    {

        public static void AddDbContext(this IServiceCollection services, string connectionString)=>
            services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(connectionString,
                    options => options.MigrationsAssembly(typeof(ConfigureServices).Namespace)))
            .AddScoped<DbConnection>(provider=>
            {
                return new SqlConnection(connectionString);
            }
            );

        public static void AddDbContext(this IServiceCollection services, string readConnectionString, string writeConnectionString) =>
       services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(writeConnectionString,
                   options => options.MigrationsAssembly(typeof(ConfigureServices).Namespace)))
             .AddScoped<DbConnection>(provider =>
             {
                 return new SqlConnection(readConnectionString);
             });
    }
}
