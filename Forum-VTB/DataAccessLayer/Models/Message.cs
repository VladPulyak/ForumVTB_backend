﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public int TopicId { get; set; }

        public int? ReplyingMessageId { get; set; }

        public string? Text { get; set; }

        public bool IsReply { get; set; }

        public ICollection<MessageFile>? Files { get; set; }

        public Topic? Topic { get; set; }

        public UserProfile? UserProfile { get; set; }

        public Message? ReplyingMessage { get; set; }
    }
}
