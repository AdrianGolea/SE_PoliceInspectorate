using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SE_PoliceInspectorate.DataAccess.Model;

namespace SE_PoliceInspectorate.DataAccess.Model
{
    public class ConferenceMessage : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }

        public int? FromId { get; set; }
        public User? From { get; set; }
        public int? ToId { get; set; }
        public User? To { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
