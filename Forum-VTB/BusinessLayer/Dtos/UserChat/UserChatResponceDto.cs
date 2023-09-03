using BusinessLayer.Dtos.Messages;
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

        public string? UserPhoto { get; set; }

        public string? AdvertId { get; set; }

        public string? AdvertTitle { get; set; }

        public string? AdvertPrice { get; set; }

        public string? AdvertPhoto { get; set; }

        public string? SectionName { get; set; }

        public string? SubsectionName { get; set; }

        public string? ChapterName { get; set; }

        public List<GetChatMessageResponceDto> Messages { get; set; }
    }
}
