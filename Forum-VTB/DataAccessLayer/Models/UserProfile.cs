using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class UserProfile : IdentityUser
    {
        public ICollection<Message>? Messages { get; set; }
    }
}