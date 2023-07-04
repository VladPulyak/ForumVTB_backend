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
        [Required(ErrorMessage = "New password must be required")]
        public string? Password { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords is different")]
        [Required(ErrorMessage = "New password must be required")]
        public string? ConfirmPassword { get; set; }
    }
}
