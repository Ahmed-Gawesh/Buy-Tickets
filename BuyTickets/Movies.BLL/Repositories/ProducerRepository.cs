using Movies.DAL;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class ProducerRepository : GenericRepository<Producer>, IProducerRepository
    {
        private readonly MovieContext dbContext;

        public ProducerRepository(MovieContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Producer> GetProducersbyName(string name)

            => dbContext.Producers.Where(P => P.FullName.ToLower().Contains(name.ToLower()));
    }
}
