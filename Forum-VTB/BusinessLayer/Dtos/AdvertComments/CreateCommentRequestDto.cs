using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.AdvertComments
{
    public class CreateCommentRequestDto
    {
        public string? AdvertId { get; set; }

        public string? Text { get; set; }

        public bool IsReply { get; set; }
    }
}
