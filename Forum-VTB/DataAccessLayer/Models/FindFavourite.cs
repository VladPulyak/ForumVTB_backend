using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FindFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? FindId { get; set; }

        public UserProfile? User { get; set; }

        public Find? Find { get; set; }
    }
}
