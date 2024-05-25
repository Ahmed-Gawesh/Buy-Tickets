using Microsoft.EntityFrameworkCore;
using Movies.DAL;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieContext dbcontext;

        public GenericRepository(MovieContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task Add(T item)
         => await dbcontext.Set<T>().AddAsync(item);

        public void Delete(T item)
            => dbcontext.Set<T>().Remove(item);

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Movie))
            {
                return (IEnumerable<T>)await dbcontext.Movies.Include(c => c.Cinema).OrderBy(c => c.Name).ToListAsync();

            }
         

            return await dbcontext.Set<T>().ToListAsync();
        }
        public async Task<T> GetbyID(int id)
        => await dbcontext.Set<T>().FindAsync(id);

        public void Update(T item)
         => dbcontext.Set<T>().Update(item);


    }
}
