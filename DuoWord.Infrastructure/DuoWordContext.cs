using DuoWord.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DuoWord.Infrastructure
{
    public class DuoWordContext : DbContext
    {
        public DbSet<Category> Categories {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationManager.AppSettings["ConnectionString.postgresql"]);
        }
    }
}
