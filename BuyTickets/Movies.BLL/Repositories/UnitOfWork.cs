using Movies.BLL.Interfaces;
using Movies.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext dbContext;

        public IActorRepository ActorRepository { get ; set ; }
        public ICinemaRepository CinemaRepository { get ; set ; }
        public IProducerRepository ProducerRepository { get; set ; }
        public IMovieRepository MovieRepository { get ; set ; }

        public UnitOfWork(MovieContext dbContext)
        {
            this.dbContext = dbContext;
            ActorRepository=new ActorRepository(dbContext);
            CinemaRepository=new CinemaRepository(dbContext);
            ProducerRepository=new ProducerRepository(dbContext);
            MovieRepository=new MovieRepository(dbContext);
        }


        public  async Task<int> Complete()
        => await dbContext.SaveChangesAsync();

        public void Dispose()
      
            =>dbContext.Dispose();
    }
}
