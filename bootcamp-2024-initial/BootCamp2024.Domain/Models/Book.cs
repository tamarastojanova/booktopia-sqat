namespace BootCamp2024.Domain.Models
{
    public class Book : EntityBase
    {
        public string Title { get; set; }
        public int? YearPublished { get; set; }
        public int? AuthorId { get; set; }
    }
}
