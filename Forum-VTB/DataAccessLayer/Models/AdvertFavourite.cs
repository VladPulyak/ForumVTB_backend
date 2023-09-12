namespace DataAccessLayer.Models
{
    public sealed class AdvertFavourite
    {
        public string? Id { get; set; }

        public string? UserId { get; set; }

        public string? AdvertId { get; set; }

        public UserProfile? User { get; set; }

        public Advert? Advert { get; set; }
    }
}
