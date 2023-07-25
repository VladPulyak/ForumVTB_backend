using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Account
{
    public class UserProfileInfoResponceDto
    {
        public string? Email { get; set; }

        public string? Photo { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
