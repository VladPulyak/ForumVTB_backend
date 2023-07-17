using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Account
{
    public class FillingAccountDataRequestDto
    {
        public string? Photo { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        public int YearOfBirth { get; set; }
    }
}
