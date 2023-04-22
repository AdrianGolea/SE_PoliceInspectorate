using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class Criminal : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Alias { get; set; }

        [Required]
        public string? NationalIdNumber { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        [Required]
        public string? Felony { get; set; }

        public string? Description { get; set; }

        public string? Sentence { get; set; }

        public DateTime? IncarcerationDate { get; set; }

        public DateTime? ExpectedReleaseDate { get; set; }

        public int? CreatedById { get; set; }

        public User? CreatedBy { get; set; }

        public int? UpdatedById { get; set; }

        public User? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

}
