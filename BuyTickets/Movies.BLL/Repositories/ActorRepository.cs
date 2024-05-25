using Movies.DAL;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class ActorRepository : GenericRepository<Actor>,IActorRepository
    {
        private readonly MovieContext dbcontext;

        public ActorRepository(MovieContext dbcontext):base(dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public IQueryable<Actor> GetActorByName(string name)

           => dbcontext.Actors.Where(A => A.FullName.ToLower().Contains(name.ToLower()));

       
    }
}
