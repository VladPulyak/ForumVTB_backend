using BusinessLayer.Dtos.Advert;
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
    }
}
