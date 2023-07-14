using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public int SubsectionId { get; set; }

        public string? Name { get; set; }

        public ICollection<TopicMessage>? Messages { get; set; }

        public Subsection? Subsection { get; set; }
    }
}
