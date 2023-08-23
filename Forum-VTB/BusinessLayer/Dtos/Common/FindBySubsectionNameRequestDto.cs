using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Common
{
    public class FindBySubsectionNameRequestDto
    {
        public string? SubsectionName { get; set; }

        public string? SectionName { get; set; }
    }
}
