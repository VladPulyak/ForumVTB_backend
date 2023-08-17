using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.WorkFiles
{
    public class AddWorkFilesRequestDto
    {
        public List<string>? FileStrings { get; set; }

        public string? WorkId { get; set; }
    }
}
