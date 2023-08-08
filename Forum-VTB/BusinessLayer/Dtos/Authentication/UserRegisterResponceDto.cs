using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Authentication
{
    public class UserRegisterResponceDto
    {
        public string? Token { get; set; }

        public string? UserEmail { get; set; }

        public string? RefreshToken { get; set; }
    }
}
