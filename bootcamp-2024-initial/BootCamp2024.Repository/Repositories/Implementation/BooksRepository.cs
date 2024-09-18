using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Interface;
namespace BootCamp2024.Repository.Repositories.Implementation
{
    public class BooksRepository : RepositoryBase<Book>, IBooksRepository
    {
        static BooksRepository()
        {
            Data.AddRange(_books);
        }

        private static readonly List<Book> _books = new List<Book>
        {
            new Book { Id = 1, AuthorId = 1, Title = "Romeo and Juliet", YearPublished = 1597 },
            new Book { Id = 2, AuthorId = 1, Title = "Hamlet", YearPublished = 1600 },
            new Book { Id = 3, AuthorId = 1, Title = "Othello", YearPublished = 1603 },
            new Book { Id = 4, AuthorId = 2, Title = "The Mysterious Affair at Styles", YearPublished = 1916 },
            new Book { Id = 5, AuthorId = 3, Title = "The Lioness and the Lily", YearPublished = 1841 },
            new Book { Id = 6, AuthorId = 4, Title = "Tycoon", YearPublished = 2011 },
            new Book { Id = 7, AuthorId = 4, Title = "Piranhas", YearPublished = 1992 }
        };
        public IEnumerable<Book> GetAllByAuthor(int authorId)
        {
            return Data.Where(x => x.AuthorId == authorId);
        }
        public Book GetBookByAuthor(int authorId, int bookId)
        {
            return Data.Where(Data => Data.AuthorId == authorId && Data.Id == bookId).FirstOrDefault();
        }
    }
}
