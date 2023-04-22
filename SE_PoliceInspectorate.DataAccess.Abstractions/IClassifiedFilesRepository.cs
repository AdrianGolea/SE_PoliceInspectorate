using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SE_PoliceInspectorate.DataAccess.Abstraction;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.DataAccess.Abstractions
{
    public interface IClassifiedFilesRepository : IBaseRepository<ClassifiedFile>
    {
        IQueryable<ClassifiedFile> GetAll();

        IQueryable<ClassifiedFile> Search(string? searchString);

        IQueryable<User> GetUsers();

        public int GetCurrentUserId();
    }
}
