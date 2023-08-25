using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.FindComments
{
    public class CreateFindCommentResponceDto
    {
        public string? CommentId { get; set; }

        public string? FindId { get; set; }

        public string? Text { get; set; }

        public string? NickName { get; set; }

        public string? UserName { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
