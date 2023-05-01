
using SE_PoliceInspectorate.DataAccess.Model;
using System.ComponentModel.DataAnnotations;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class PoliceStation : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ICollection<User> User { get; set; }
    }
}