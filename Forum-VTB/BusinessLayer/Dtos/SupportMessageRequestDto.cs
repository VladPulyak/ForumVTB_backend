using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class SupportMessageRequestDto
    {
        public string? Text { get; set; }

        public IEnumerable <string>? Files { get; set; }
    }
}
