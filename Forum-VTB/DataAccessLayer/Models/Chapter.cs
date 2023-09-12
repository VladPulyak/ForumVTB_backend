namespace DataAccessLayer.Models
{
    public sealed class Chapter
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Section>? Sections { get; set; }
    }
}
