using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class CheckPasswordRequestDto
    {
        [Required(ErrorMessage = "Current password must be required")]
        public string? OldPassword { get; set; }
    }
}
