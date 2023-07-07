using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class UserProfile : IdentityUser
    {
        public string? Photo { get; set; }

        public DateTime BirthDate { get; set; }

        public string? NickName { get; set; }

        public ICollection<Message>? Messages { get; set; }

        public ICollection<UserMessage>? SentMessages { get; set; }

        public ICollection<UserMessage>? ReceivedMessages { get; set; }
    }
}