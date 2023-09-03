using BusinessLayer.Dtos.AdvertComments;
using BusinessLayer.Dtos.AdvertFiles;
using BusinessLayer.Dtos.FindComments;
using BusinessLayer.Dtos.FindFIles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Find
{
    public class GetFindCardResponceDto
    {
        public string? FindId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public string? UserPhoto { get; set; }

        public string? SubsectionName { get; set; }

        public string? Price { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsFavourite { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MainPhoto { get; set; }

        public string? Status { get; set; }

        public List<GetFindCommentResponceDto>? Comments { get; set; }

        public List<GetFindFileResponceDto>? Files { get; set; }

    }
}
