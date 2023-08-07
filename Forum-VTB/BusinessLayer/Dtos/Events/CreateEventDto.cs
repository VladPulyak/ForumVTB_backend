using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Events
{
    public class CreateEventDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? OtherInfo { get; set; }

        public string? Poster { get; set; }
    }
}
