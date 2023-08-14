using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class WorkComment
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? WorkId { get; set; }

        public string? ParentCommentId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Work? Work { get; set; }

        public UserProfile? UserProfile { get; set; }

        public WorkComment? ParentComment { get; set; }

        public ICollection<WorkComment>? Replies { get; set; }

    }
}
