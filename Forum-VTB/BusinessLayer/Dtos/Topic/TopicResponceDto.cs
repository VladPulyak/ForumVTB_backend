using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Topic
{
    public class TopicResponceDto
    {
        public string? TopicId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsFavourite { get; set; }

        public string? MainPhoto { get; set; }

        public DateTime? DateOfLastMessage { get; set; }

        public int? MessagesCount { get; set; }

        public string? SectionName { get; set; }

        public string? SubsectionName { get; set; }
    }
}
