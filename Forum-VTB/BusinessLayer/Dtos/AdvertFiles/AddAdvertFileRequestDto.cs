using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.AdvertFiles
{
    public class AddAdvertFileRequestDto
    {
        public List<string>? FileStrings { get; set; }

        public string? AdvertId { get; set; }
    }
}
