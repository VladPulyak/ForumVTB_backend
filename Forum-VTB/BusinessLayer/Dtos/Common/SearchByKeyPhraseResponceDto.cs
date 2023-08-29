using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Find;
using BusinessLayer.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Common
{
    public class SearchByKeyPhraseResponceDto
    {
        public List<AdvertResponceDto>? Adverts { get; set; }

        public List<JobResponceDto>? Jobs { get; set; }

        public List<FindResponceDto>? Finds { get; set; }
    }
}
