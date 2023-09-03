using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Topic
{
    public class UpdateTopicRequestDto
    {
        public string? TopicId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? MainPhoto { get; set; }

        public string? SubsectionName { get; set; }

        public string? SectionName { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
