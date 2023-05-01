using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using global::PoliceInspectorate.DataAccess.Model;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using System.Data.Entity;
using SE_PoliceInspectorate.DataAccess.Abstraction;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly PoliceInspectorateContext dbContext;
        public BaseRepository(PoliceInspectorateContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public T Add(T element)
        {
            var returnEntity = dbContext.Set<T>()
                                        .Add(element)
                                     .Entity;

            return returnEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var itemToRemove = await GetByIdAsync(id);
            if (itemToRemove != null)
            {
                dbContext.Set<T>().Remove(itemToRemove);
                return true;
            }
            return false;
        }


        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await GetAll()
                             .FirstAsync(entity => entity.Id == id);
        }

        public virtual T Update(T elementToUpdate)
        {
            var returnEntity = dbContext.Set<T>()
                                        .Update(elementToUpdate)
                                     .Entity;

            return returnEntity;
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }

}
