using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AdvertComment
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? AdvertId { get; set; }

        public string? ParentCommentId { get; set; }

        public string? Text { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Advert? Advert { get; set; }

        public UserProfile? UserProfile { get; set; }

        public AdvertComment? ParentComment { get; set; }

        public ICollection<AdvertComment>? Replies { get; set; }
    }
}
