using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Job
{
    public class JobResponceDto
    {
        public string? JobId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? Status { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsFavourite { get; set; }

        public string? MainPhoto { get; set; }

        public string? SectionName { get; set; }

        public string? SubsectionName { get; set; }
    }
}
