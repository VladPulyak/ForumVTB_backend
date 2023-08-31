using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicEntity = DataAccessLayer.Models.Topic;

namespace BusinessLayer.Dtos.TopicFile
{
    public class AddMissingTopicFilesRequestDto
    {
        public TopicEntity? Topic { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
