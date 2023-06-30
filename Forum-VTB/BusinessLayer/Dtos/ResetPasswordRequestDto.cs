using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class ResetPasswordRequestDto
    {
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
