using BootCamp2024.Domain.Models;

namespace BootCamp2024.Repository.Repositories.Interface
{
    public interface IBooksRepository : IRepositoryBase<Book>
    {
        public IEnumerable<Book> GetAllByAuthor(int authorId);
        public Book GetBookByAuthor(int authorId, int bookId);

    }
}
