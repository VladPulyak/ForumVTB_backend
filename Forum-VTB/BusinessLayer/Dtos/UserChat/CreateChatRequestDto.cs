using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.UserChat
{
    public class CreateChatRequestDto
    {
        public string? AdvertId { get; set; }

        public string? ReceiverUsername { get; set; }

        public string? ChapterName { get; set; }
    }
}
