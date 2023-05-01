using Microsoft.AspNetCore.Http;
using SE_PoliceInspectorate.DataAccess.Abstractions;
using SE_PoliceInspectorate.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    public class ConferenceMessageRepository : BaseRepository<ConferenceMessage>, IConferenceMessageRepository
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public ConferenceMessageRepository(PoliceInspectorateContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<ConferenceMessage> GetMessages(int Receiver)
        {
            var currentUserId = int.Parse(this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return dbContext.Set<ConferenceMessage>().Where(cm => (cm.FromId == Receiver && cm.ToId == currentUserId) ||
                                                                    (cm.FromId == currentUserId && cm.ToId == Receiver))
                                                     .OrderBy(cm => cm.TimeStamp);
        }

        public int GetCurrentUserId()
        {
            return int.Parse(this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public IQueryable<User> GetUsers()
        {
            var currentUserId = int.Parse(this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return dbContext.Set<User>().Include(x => x.PoliceStation).Where(u => u.Id != currentUserId).AsNoTracking();
        }
    }
}