using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class UserProfile : IdentityUser
    {
        public string? Photo { get; set; }

        public DateTime BirthDate { get; set; }

        public string? NickName { get; set; }

        public long? ChatId { get; set; }

        public ICollection<AdvertComment>? AdvertComments { get; set; }

        public ICollection<UserMessage>? SentMessages { get; set; }

        public ICollection<UserMessage>? ReceivedMessages { get; set; }

        public ICollection<Advert>? Adverts { get; set; }

        public ICollection<AdvertFavourite>? AdvertFavourites { get; set; }

        public UserTheme? Theme { get; set; }

        public ICollection<UserChat>? ChatsAsFirstUser { get; set; }

        public ICollection<UserChat>? ChatsAsSecondUser { get; set; }

        public ICollection<WorkComment>? WorkComments { get; set; }

        public ICollection<Work>? Works { get; set; }

        public ICollection<WorkFavourite>? WorkFavourites { get; set; }

    }
}