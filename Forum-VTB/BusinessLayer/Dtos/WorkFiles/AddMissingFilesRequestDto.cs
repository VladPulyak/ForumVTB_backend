using Service = DataAccessLayer.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.WorkFiles
{
    public class AddMissingFilesRequestDto
    {
        public Service? Work { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
