using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.FindFIles
{
    public class UpdateFindFilesRequestDto
    {
        public List<string>? FileStrings { get; set; }

        public string? FindId { get; set; }
    }
}
