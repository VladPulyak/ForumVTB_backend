using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AdvertFile
    {
        public int Id { get; set; }

        public string? AdvertId { get; set; }

        public string? FileURL { get; set; }

        public Advert? Advert { get; set; }
    }
}
