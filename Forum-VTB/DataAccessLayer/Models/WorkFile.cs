using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class WorkFile
    {
        public string? Id { get; set; }

        public string? WorkId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Work? Work { get; set; }
    }
}
