using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Event
    {
        public string? Id { get; set; }

        public int? SubsectionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Poster { get; set; }

        public string? Address { get; set; }

        public string? Price { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Subsection? Subsection { get; set; }
    }
}
