using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class EmailSenderDto
    {
        public string? ReceiverEmail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }
    }
}
