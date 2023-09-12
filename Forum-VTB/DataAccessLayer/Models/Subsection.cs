namespace DataAccessLayer.Models
{
    public sealed class Subsection
    {
        public int Id { get; set; }

        public int SectionId { get; set; }

        public string? Name { get; set; }

        public ICollection<Advert>? Adverts { get; set; }

        public Section? Section { get; set; }

        public ICollection<Event>? Events { get; set; }

        public ICollection<Job>? Jobs { get; set; }

        public ICollection<Find>? Finds { get; set; }

        public ICollection<Topic>? Topics { get; set; }
    }
}
