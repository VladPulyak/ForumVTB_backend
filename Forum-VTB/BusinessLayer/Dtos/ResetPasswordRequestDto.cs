using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class ResetPasswordRequestDto
    {
        public string? Password { get; set; }
    }
}
