using Movies.DAL;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly MovieContext dbContext;

        public MovieRepository(MovieContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }
       
        public IQueryable<Movie> GetMoviesByName(string name)

            => dbContext.Movies.Where(M => M.Name.ToLower().Contains(name.ToLower()));
    }
}
