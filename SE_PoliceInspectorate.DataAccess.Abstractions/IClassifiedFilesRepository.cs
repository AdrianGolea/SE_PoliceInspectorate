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
        IQueryable<User> GetUsers();
        IQueryable<ClassifiedFile> Search(string? searchString);
        int GetCurrentUserId();
    }
}