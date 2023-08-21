using Service = DataAccessLayer.Models.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.JobFiles
{
    public class AddMissingJobFilesRequestDto
    {
        public Service? Job { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
