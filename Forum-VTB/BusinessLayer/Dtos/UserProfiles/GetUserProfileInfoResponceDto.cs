using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.UserProfiles
{
    public class GetUserProfileInfoResponceDto
    {
        public string? Photo { get; set; }

        public string? NickName { get; set; }

        public string? Username { get; set; }
    }
}
