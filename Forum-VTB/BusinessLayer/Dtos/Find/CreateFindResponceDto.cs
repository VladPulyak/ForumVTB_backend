using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.FindFIles;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Find
{
    public class CreateFindResponceDto
    {
        public string? FindId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? MainPhoto { get; set; }

        public string? Status { get; set; }

        public List<FindComment>? Comments { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<GetFindFileResponceDto>? Files { get; set; }
    }
}
