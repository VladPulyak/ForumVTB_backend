using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class MessageFile
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        public string? FileURL { get; set; }

        public Message? Message { get; set; }

    }
}
