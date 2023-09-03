using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FindComment
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? FindId { get; set; }

        public string? ParentCommentId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Find? Find { get; set; }

        public UserProfile? UserProfile { get; set; }

        public FindComment? ParentComment { get; set; }

        public ICollection<FindComment>? Replies { get; set; }
    }
}
