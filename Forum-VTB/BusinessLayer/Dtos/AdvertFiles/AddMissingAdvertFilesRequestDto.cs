using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advertisement = DataAccessLayer.Models.Advert;

namespace BusinessLayer.Dtos.AdvertFiles
{
    public class AddMissingAdvertFilesRequestDto
    {
        public Advertisement? Advert { get; set; }

        public List<string>? FileStrings { get; set; }
    }
}
