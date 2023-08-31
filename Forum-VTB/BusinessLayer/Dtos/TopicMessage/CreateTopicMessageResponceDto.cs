using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.TopicMessage
{
    public class CreateTopicMessageResponceDto
    {
        public string? MessageId { get; set; }

        public string? TopicId { get; set; }

        public string? Text { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
