using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UserChat
    {
        public string? Id { get; set; }

        public string? FirstUserId { get; set; }

        public string? SecondUserId { get; set; }

        public string? AdvertId { get; set; }

        public string? ChapterName { get; set; }

        public UserProfile? FirstUser { get; set; }

        public UserProfile? SecondUser { get; set; }

        public ICollection<UserMessage>? Messages { get; set; }

        [NotMapped]
        public Advert? Advert { get; set; }

        [NotMapped]
        public Job? Job { get; set; }

        [NotMapped]
        public Find? Find { get; set; }
    }
}
