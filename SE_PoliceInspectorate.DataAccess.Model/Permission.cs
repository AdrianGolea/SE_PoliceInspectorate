using SE_PoliceInspectorate.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class Permission : IEntity
    {
        public Permission()
        {
            this.Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}
