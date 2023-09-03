using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindEntity = DataAccessLayer.Models.Find;

namespace BusinessLayer.Dtos.FindFIles
{
    public class AddMissingFindFilesRequestDto
    {
        public FindEntity? Find { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
