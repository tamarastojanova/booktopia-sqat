using BootCamp2024.Domain.Models;

namespace BootCamp2024.Service.Interface
{
	public interface IBooksService
	{
		public void Create(Book book);
		public bool Delete(int id);
		public IEnumerable<Book> GetAll();
		public Book GetById(int id);
		public bool Update(Book book, int id);
		IEnumerable<Book> GetAllByAuthor(int authorId);
		Book GetBookByAuthor(int authorId, int bookId);
	}
}
