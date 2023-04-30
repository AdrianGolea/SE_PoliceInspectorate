using Microsoft.EntityFrameworkCore;
using SE_PoliceInspectorate.DataAccess.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    public class PoliceInspectorateContext : IdentityDbContext<User, Role, int>
    {
        public PoliceInspectorateContext(DbContextOptions<PoliceInspectorateContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<ConferenceMessage> ConferenceMessage { get; set; }
        public DbSet<ClassifiedFile> ClassifiedFile { get; set; }
        public DbSet<PoliceStation> PoliceStation { get; set; }
    }
}






