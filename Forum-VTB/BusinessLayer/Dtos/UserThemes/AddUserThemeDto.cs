using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.UserThemes
{
    public class AddUserThemeDto
    {
        public string? Theme { get; set; }

        public string? UserId { get; set; }
    }
}
