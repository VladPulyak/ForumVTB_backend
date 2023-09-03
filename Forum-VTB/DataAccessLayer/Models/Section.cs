using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Section
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? ChapterId { get; set; }

        public ICollection<Subsection>? Subsections { get; set; }

        public Chapter? Chapter { get; set; }
    }
}
