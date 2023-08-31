using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.TopicFile;
using BusinessLayer.Dtos.TopicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Topic
{
    public class UpdateTopicResponceDto
    {
        public string? TopicId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? MainPhoto { get; set; }

        public bool? IsFavourite { get; set; }

        public List<GetTopicMessageResponceDto>? Messages { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<GetTopicFileResponceDto>? Files { get; set; }
    }
}
