using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Messages
{
    public class GetChatMessageResponceDto
    {
        public string? SenderId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
