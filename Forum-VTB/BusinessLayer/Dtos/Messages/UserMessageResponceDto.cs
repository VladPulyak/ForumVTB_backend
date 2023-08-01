﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Messages
{
    public class UserMessageResponceDto
    {
        public string? SenderUserName { get; set; }

        public string? ReceiverUserName { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
