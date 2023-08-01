using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Messages
{
    public class UpdateMessageRequestDto
    {
        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
