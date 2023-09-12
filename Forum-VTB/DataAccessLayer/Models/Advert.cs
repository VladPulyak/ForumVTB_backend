namespace DataAccessLayer.Models
{
    public sealed class Advert
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public int? SubsectionId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Price { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string? Status { get; set; }

        public string? MainPhoto { get; set; }

        public ICollection<AdvertFile>? Files { get; set; }

        public UserProfile? User { get; set; }

        public ICollection<AdvertComment>? AdvertComments { get; set; }

        public Subsection? Subsection { get; set; }

        public ICollection<AdvertFavourite>? Favourites { get; set; }
    }
}
