using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RefreshToken
    {
        public string? Token { get; set; }

        public DateTime DateOfCreating { get; set; }

        public DateTime DateOfExpiring { get; set; }
    }
}
