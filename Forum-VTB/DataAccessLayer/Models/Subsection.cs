using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Subsection
    {
        public int Id { get; set; }

        public int SectionId { get; set; }

        public string? Name { get; set; }

        public ICollection<Topic>? Topics { get; set; }

        public Section? Section { get; set; }
    }
}
