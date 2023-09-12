using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public sealed class UserProfile : IdentityUser
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

        public ICollection<Job>? Jobs { get; set; }

        public ICollection<JobFavourite>? JobFavourites { get; set; }

        public ICollection<Find>? Finds { get; set; }

        public ICollection<FindFavourite>? FindFavourites { get; set; }

        public ICollection<FindComment>? FindComments { get; set; }

        public ICollection<Topic>? Topics { get; set; }

        public ICollection<TopicFavourite>? TopicFavourites { get; set; }

        public ICollection<TopicMessage>? TopicMessages { get; set; }


    }
}