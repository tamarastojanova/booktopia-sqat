using BootCamp2024.Domain.Models;

namespace BootCamp2024.Domain.Extensions
{
    public static class BookValidator
    {
        public static void Validate(this Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            if (string.IsNullOrEmpty(book.Title) || book.Title.Length > 25)
            {
                throw new ArgumentException("Book must have a title and it should be less than 25 characters");
            }
            if(book.YearPublished>DateTime.Now.Year || book.YearPublished == null)
            {
                throw new ArgumentException("Year published should be less than or equal to the current year");
            }
            if(book.AuthorId == null)
            {
                throw new ArgumentException("Book must have an author");
            }
        }
    }
}
