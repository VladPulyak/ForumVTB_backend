using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.UserChat
{
    public class UserChatResponceDto
    {
        public string? ChatId { get; set; }

        public string? Username { get; set; }

        public string? NickName { get; set; }

        public string? Photo { get; set; }
    }
}
