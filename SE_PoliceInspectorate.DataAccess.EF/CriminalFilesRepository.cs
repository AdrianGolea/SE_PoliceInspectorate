using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PoliceInspectorate.DataAccess.Abstractions;
using PoliceInspectorate.DataModel;
using PoliceInspectorate.DataAccess.Model;
using SE_PoliceInspectorate.DataAccess.Model;
using PoliceInspectorate.DataModel;
using PoliceInspectorate.DataAccess.Abstraction;

namespace PoliceInspectorate.DataAccess.EF
{
   
   
        public class ClassifiedFileRepository : BaseRepository<ClassifiedFile>, IClassifiedFilesRepository
        {
            public readonly IHttpContextAccessor _httpContextAccessor;
            public ClassifiedFileRepository(PoliceInspectorateContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
            {
                this._httpContextAccessor = httpContextAccessor;
            }

            public override IQueryable<ClassifiedFile> GetAll()
            {
                return dbContext.Set<ClassifiedFile>()
                                .Include(classifiedFile => classifiedFile.CreatedBy)
                                .Include(classifiedFile => classifiedFile.UpdatedBy)
                                .AsNoTracking();
            }

            public IQueryable<ClassifiedFile> Search(string searchString = null)
            {
                if (searchString == null)
                    return GetAll();

                return GetAll().Where(x => x.InmateName.Contains(searchString) ||
                                                x.Felony.Contains(searchString) ||
                                                x.Title.Contains(searchString) ||
                                                x.Description.Contains(searchString) ||
                                                x.Sentence.Contains(searchString));
            }

            public IQueryable<User> GetUsers()
            {
                return dbContext.Set<User>().AsNoTracking();
            }

            public int GetCurrentUserId()
            {
                return int.Parse(this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
    

}
