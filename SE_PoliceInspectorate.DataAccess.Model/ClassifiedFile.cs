using PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class ClassifiedFile : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string InmateName { get; set; }
        public string Felony { get; set; }
        public string Sentence { get; set; }
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