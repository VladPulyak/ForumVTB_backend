using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Find
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public int? SubsectionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string? Status { get; set; }

        public string? MainPhoto { get; set; }

        public ICollection<FindFile>? Files { get; set; }

        public UserProfile? User { get; set; }

        public ICollection<FindComment>? FindComments { get; set; }

        public Subsection? Subsection { get; set; }

        public ICollection<FindFavourite>? Favourites { get; set; }

        //public ICollection<UserChat>? Chats { get; set; }
    }
}
