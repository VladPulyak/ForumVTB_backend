using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.TopicFile;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicMessageEntity = DataAccessLayer.Models.TopicMessage;

namespace BusinessLayer.Dtos.Topic
{
    public class CreateTopicResponceDto
    {
        public string? TopicId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? MainPhoto { get; set; }

        public List<TopicMessageEntity>? Messages { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<GetTopicFileResponceDto>? Files { get; set; }
    }
}
