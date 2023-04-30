using Microsoft.AspNetCore.Identity;
using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role()
        {
            this.Permissions = new HashSet<Permission>();
        }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
