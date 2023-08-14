using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class WorkFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? WorkId { get; set; }

        public UserProfile User { get; set; }

        public Work Work { get; set; }

    }
}
