using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Find
{
    public class CreateFindRequestDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? MainPhoto { get; set; }

        public string? SubsectionName { get; set; }

        public string? SectionName { get; set; }

        public string? PhoneNumber { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
