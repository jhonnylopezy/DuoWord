using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuoWord.Infrastructure.Data
{
    public class CommandRepository<T>(AppDbContext dbContext):CommandBaseRepository<T>(dbContext) where T: class
    {
        private readonly AppDbContext _dbContext = dbContext;
    }
}
