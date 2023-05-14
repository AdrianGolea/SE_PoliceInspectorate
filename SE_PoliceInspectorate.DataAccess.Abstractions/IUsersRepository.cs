using SE_PoliceInspectorate.DataAccess.Abstraction;
using SE_PoliceInspectorate.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_PoliceInspectorate.DataAccess.Abstractions
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        IQueryable<User> GetAll();
        IQueryable<User> Update(User user);
        IQueryable<PoliceStation> GetStations();
    }

}

