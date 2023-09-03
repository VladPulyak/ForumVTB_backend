using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.JobFiles
{
    public class AddJobFilesRequestDto
    {
        public List<string>? FileStrings { get; set; }

        public string? JobId { get; set; }
    }
}
