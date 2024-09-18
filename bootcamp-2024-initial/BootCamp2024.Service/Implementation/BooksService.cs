using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Interface;
using BootCamp2024.Service.Interface;

namespace BootCamp2024.Service.Implementation
{
	public class BooksService : IBooksService
	{
		private readonly IBooksRepository _booksRepository;
		public BooksService(IBooksRepository booksRepository)
		{
			_booksRepository = booksRepository;
		}
		public void Create(Book book)
		{
			_booksRepository.Create(book);
		}

		public bool Delete(int id)
		{
			return _booksRepository.Delete(id);
		}

		public IEnumerable<Book> GetAll()
		{
			return _booksRepository.GetAll();
		}

		public IEnumerable<Book> GetAllByAuthor(int authorId)
		{
			return _booksRepository.GetAllByAuthor(authorId);
		}

		public Book GetBookByAuthor(int authorId, int bookId)
		{
			return _booksRepository.GetBookByAuthor(authorId, bookId);
		}

		public Book GetById(int id)
		{
			return _booksRepository.GetById(id);
		}

		public bool Update(Book book, int id)
		{
			return _booksRepository.Update(book, id);
		}
	}
}
