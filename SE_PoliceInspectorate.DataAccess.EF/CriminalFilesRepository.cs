using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SE_PoliceInspectorate.DataAccess.Model;
using SE_PoliceInspectorate.DataAccess.Abstractions;

namespace SE_PoliceInspectorate.DataAccess.EF
{
   
   
        public class CriminalFilesRepository : BaseRepository<Criminal>, ICriminalFilesRepository
        {
            public readonly IHttpContextAccessor _httpContextAccessor;
        
        public CriminalFilesRepository(PoliceInspectorateContext dbContext, IHttpContextAccessor httpContextAccessor)
    : base(dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override IQueryable<Criminal> GetAll()
            {
                return dbContext.Set<Criminal>()
                                .Include(classifiedFile => classifiedFile.CreatedBy)
                                .Include(classifiedFile => classifiedFile.UpdatedBy)
                                .AsNoTracking();
            }

            public IQueryable<Criminal> Search(string? searchString)
            {
                if (string.IsNullOrEmpty(searchString))
                    return GetAll();

                return GetAll().Where(x => x.Name.Contains(searchString) ||
                                           x.Alias.Contains(searchString) ||
                                           x.NationalIdNumber.Contains(searchString) ||
                                           x.Address.Contains(searchString) ||
                                           x.Phone.Contains(searchString) ||
                                           x.Email.Contains(searchString) ||
                                           x.Felony.Contains(searchString) ||
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
