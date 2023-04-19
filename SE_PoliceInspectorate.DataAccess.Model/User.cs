using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using PoliceInspectorate.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceInspectorate.DataAccess.Model
{
    public class User : IdentityUser<int>, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int? PoliceStationId { get; set; }
        public PoliceStation? PoliceStation { get; set; }

        /*[InverseProperty("From")]
        public ICollection<ConferenceMessage> SentMessages { get; set; }
        [InverseProperty("To")]
        public ICollection<ConferenceMessage> ReceivedMessages { get; set; }*/
        [InverseProperty("CreatedBy")]
        public ICollection<ClassifiedFile> CreatedFiles { get; set; }
        [InverseProperty("UpdatedBy")]
        public ICollection<ClassifiedFile> UpdatedFiles { get; set; }

        [InverseProperty("UpdatedBy")]
        public ICollection<ClassifiedFile> UpdatedCriminalFiles { get; set; }


    }
}
