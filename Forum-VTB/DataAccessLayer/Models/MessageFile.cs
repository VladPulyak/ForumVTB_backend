using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class MessageFile
    {
        public string? Id { get; set; }

        public string? MessageId { get; set; }

        public string? FileURL { get; set; }

        public UserMessage? UserMessage { get; set; }
    }
}
