using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UserMessage
    {
        public string? Id { get; set; }

        public string? SenderId { get; set; }

        public string? ReceiverId { get; set; }

        public string? ParentMessageId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public UserProfile? Sender { get; set; }

        public UserProfile? Receiver { get; set; }

        public UserMessage? ParentMessage { get; set; }

        public ICollection<UserMessage>? Replies { get; set; }

        public ICollection<MessageFile>? Files { get; set; }
    }
}
