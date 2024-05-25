using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetbyID(int id);

        Task Add(T item);

        void Update(T item);

        void Delete(T item);
    }
}
