using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TopicMessage
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? TopicId { get; set; }

        public string? ParentMessageId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Topic? Topic { get; set; }

        public UserProfile? UserProfile { get; set; }

        public TopicMessage? ParentMessage { get; set; }

        public ICollection<TopicMessage>? Replies { get; set; }

    }
}
