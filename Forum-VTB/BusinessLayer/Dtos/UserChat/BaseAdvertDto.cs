using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.UserChat
{
    public class BaseAdvertDto
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? Price { get; set; }

        public string? MainPhoto { get; set; }
    }
}
