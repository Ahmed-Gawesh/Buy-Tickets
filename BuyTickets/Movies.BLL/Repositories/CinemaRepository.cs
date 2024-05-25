using Movies.DAL;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class CinemaRepository : GenericRepository<Cinema>, ICinemaRepository
    {
        private readonly MovieContext dbContext;

        public CinemaRepository(MovieContext dbContext):base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Cinema> GetCinemasByName(string name)

            => dbContext.Cinemas.Where(C => C.FullName.ToLower().Contains(name.ToLower()));
    }
}
