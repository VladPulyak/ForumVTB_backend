using BusinessLayer.Dtos.Advert;
using BusinessLayer.Dtos.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Common
{
    public class GetAllUserAdvertsResponceDto
    {
        public List<AdvertResponceDto>? Adverts { get; set; }

        public List<JobResponceDto>? Jobs { get; set; }
    }
}
