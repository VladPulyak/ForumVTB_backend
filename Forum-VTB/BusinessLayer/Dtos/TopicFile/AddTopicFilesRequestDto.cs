using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.TopicFile
{
    public class AddTopicFilesRequestDto
    {
        public List<string>? FileStrings { get; set; }

        public string? TopicId { get; set; }
    }
}
