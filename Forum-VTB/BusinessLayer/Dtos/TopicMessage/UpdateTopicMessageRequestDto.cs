using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.TopicMessage
{
    public class UpdateTopicMessageRequestDto
    {
        public string? MessageId { get; set; }

        public string? Text { get; set; }
    }
}
