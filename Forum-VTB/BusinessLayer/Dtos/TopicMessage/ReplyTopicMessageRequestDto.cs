using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.TopicMessage
{
    public class ReplyTopicMessageRequestDto
    {
        public string? TopicId { get; set; }

        public string? Text { get; set; }

        public string? ParentMessageId { get; set; }
    }
}
