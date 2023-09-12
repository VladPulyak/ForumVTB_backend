namespace DataAccessLayer.Models
{
    public sealed class AdvertFile
    {
        public string? Id { get; set; }

        public string? AdvertId { get; set; }

        public string? FileURL { get; set; }

        public DateTime DateOfCreation { get; set; }

        public Advert? Advert { get; set; }
    }
}
