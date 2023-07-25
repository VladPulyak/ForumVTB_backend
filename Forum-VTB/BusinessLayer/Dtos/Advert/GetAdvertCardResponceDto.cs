using BusinessLayer.Dtos.AdvertComments;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.Advert
{
    public class GetAdvertCardResponceDto
    {
        public string? AdvertId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public string? UserPhoto { get; set; }

        public string? SubsectionName { get; set; }

        public string? Price { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<GetCommentResponceDto>? Comments { get; set; }

        public List<AdvertFile>? Files { get; set; }
    }
}
