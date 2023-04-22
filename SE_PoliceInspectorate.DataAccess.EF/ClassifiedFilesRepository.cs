using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    public class ClassifiedFilesRepository : BaseRepository<ClassifiedFile>, IClassifiedFilesRepository
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public ClassifiedFilesRepository(PoliceInspectorateContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
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

        public IQueryable<ClassifiedFile> Search(string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
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

