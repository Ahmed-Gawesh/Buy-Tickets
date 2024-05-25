using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public interface ICinemaRepository:IGenericRepository<Cinema>
    {
       public IQueryable<Cinema> GetCinemasByName(string name);
    }
}
