namespace DataAccessLayer.Models
{
    public sealed class Section
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? ChapterId { get; set; }

        public ICollection<Subsection>? Subsections { get; set; }

        public Chapter? Chapter { get; set; }
    }
}
