using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class ChangePasswordRequestDto
    {
        [Required(ErrorMessage = "Current password must be required")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New password must be required")]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords is different")]
        [Required(ErrorMessage = "New password must be required")]
        public string? ConfirmPassword { get; set; }
    }
}
