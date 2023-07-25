using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Advert
{
    public class UpdateAdvertResponceDto
    {
        public string? AdvertId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public List<AdvertComment>? Comments { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
