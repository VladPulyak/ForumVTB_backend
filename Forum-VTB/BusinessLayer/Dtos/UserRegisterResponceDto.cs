using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class UserRegisterResponceDto
    {
        [JsonIgnore]
        public List<IdentityError> Errors { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
