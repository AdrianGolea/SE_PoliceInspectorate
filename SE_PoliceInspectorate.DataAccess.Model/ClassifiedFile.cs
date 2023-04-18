using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;

namespace PoliceInspectorate.DataModel
{
    public class ClassifiedFile : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string InmateName { get; set; } = string.Empty;
        public string Felony { get; set; } = string.Empty;
        public string Sentence { get; set; } = string.Empty;
        public DateTime? IncarcerationDate { get; set; }
        public DateTime? ExpectedReleaseDate { get; set; }
        public int? CreatedById { get; set; }
        //   public User? CreatedBy { get; set; }
        public int? UpdatedById { get; set; }
        //   public User? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}