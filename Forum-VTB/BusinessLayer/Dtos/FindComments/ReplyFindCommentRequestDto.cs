using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.FindComments
{
    public class ReplyFindCommentRequestDto
    {
        public string? FindId { get; set; }

        public string? Text { get; set; }

        public string? ParentCommentId { get; set; }
    }
}
