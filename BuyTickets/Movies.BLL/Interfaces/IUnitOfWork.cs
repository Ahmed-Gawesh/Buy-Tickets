using Movies.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
         IActorRepository ActorRepository { get; set; }
         ICinemaRepository CinemaRepository { get; set; }
        IProducerRepository ProducerRepository { get; set; }
        IMovieRepository MovieRepository { get; set; }

        public Task<int> Complete();

    }
}
