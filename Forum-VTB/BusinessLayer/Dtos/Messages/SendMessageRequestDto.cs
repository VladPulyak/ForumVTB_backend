using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Messages
{
    public class SendMessageRequestDto
    {
        public string? ReceiverUsername { get; set; }

        public string? Text { get; set; }
    }
}
