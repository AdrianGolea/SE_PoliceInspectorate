using Microsoft.AspNetCore.Identity;
using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class User : IdentityUser<int>, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

       
        public int? PoliceStationId { get; set; }
        public PoliceStation? PoliceStation { get; set; }

        //[InverseProperty("From")]

        //public ICollection<ClassifiedFile> CreatedFiles { get; set; }
        //[InverseProperty("UpdatedBy")]
        //public ICollection<ClassifiedFile> UpdatedFiles { get; set; }

        [InverseProperty(nameof(ClassifiedFile.CreatedBy))]
        public ICollection<ClassifiedFile> CreatedFiles { get; set; }

        [InverseProperty(nameof(ClassifiedFile.UpdatedBy))]
        public ICollection<ClassifiedFile> UpdatedFiles { get; set; }
    }
}

