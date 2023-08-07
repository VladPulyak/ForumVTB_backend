using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Authentication
{
    public class AuthResponceDto
    {
        public string? Token { get; set; }

        public string? UserEmail { get; set; }

        public string? RefreshToken { get; set; }

        public string? Theme { get; set; }
    }
}
