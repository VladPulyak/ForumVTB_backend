using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class JobFile
    {
        public string? Id { get; set; }

        public string? JobId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Job? Job { get; set; }
    }
}
