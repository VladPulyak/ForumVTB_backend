using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Job
{
    public class UserJobResponceDto
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public DateTime DateOfCreation { get; set; }

    }
}
