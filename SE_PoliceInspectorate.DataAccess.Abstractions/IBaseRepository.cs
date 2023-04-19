using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceInspectorate.DataAccess.Abstraction
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        T Add(T element);
        Task<bool> Delete(int id);
        T Update(T elementToUpdate);
        Task SaveAsync();
    }
}
