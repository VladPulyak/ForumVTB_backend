using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TopicFile
    {
        public string? Id { get; set; }

        public string? TopicId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Topic? Topic { get; set; }
    }
}
