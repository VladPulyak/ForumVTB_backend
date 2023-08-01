using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos.AdvertComments
{
    public class DeleteCommentRequestDto
    {
        public DateTime DateOfCreation { get; set; }

        public string? Username { get; set; }
    }
}
