using BusinessLayer.Dtos.AdvertComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.FindComments
{
    public class GetFindCommentResponceDto
    {
        public string? CommentId { get; set; }

        public string? UserPhoto { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public List<Re_ReplyFindCommentDto>? Replies { get; set; }
    }
}
