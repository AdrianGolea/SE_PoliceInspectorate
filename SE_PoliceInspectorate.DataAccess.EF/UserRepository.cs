using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    public class UserRepository : BaseRepository<User>, IUsersRepository
    {
        public UserRepository(PoliceInspectorateContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<User> GetAll()
        {
            return dbContext.Set<User>()
                            .Include(user => user.PoliceStation).AsNoTracking();
        }

        public IQueryable<PoliceStation> GetStations()
        {
            return dbContext.Set<PoliceStation>().AsNoTracking();
        }

       

        public override User Update(User user)
        {
            var entity = dbContext.Set<User>().Where(x => x.Id == user.Id).First();

            entity.UserName = user.UserName;
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
            entity.PoliceStationId = user.PoliceStationId;

            dbContext.SaveChanges();

            return entity;
        }
    }
}
