using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FindFile
    {
        public string? Id { get; set; }

        public string? FindId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Find? Find { get; set; }
    }
}
