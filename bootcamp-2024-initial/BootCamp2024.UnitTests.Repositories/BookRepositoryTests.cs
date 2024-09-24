using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Repository.Repositories.Interface;

namespace BootCamp2024.UnitTests.Repositories
{
	[TestFixture]
	public class BookRepositoryTests
	{
		private readonly IBooksRepository _booksRepository;

		public BookRepositoryTests()
		{
			_booksRepository = new BooksRepository();
		}

		[Test]
		public void GetAll_ShouldReturnBooks_ListMutant()
		{
			BooksRepository.ResetBooks();

			var books = _booksRepository.GetAll().ToList();

			Assert.That(books, Is.Not.Null);
			Assert.That(books.Count(), Is.EqualTo(7));

			Assert.That(books[0].AuthorId, Is.EqualTo(1));
			Assert.That(books[1].AuthorId, Is.EqualTo(1));
			Assert.That(books[2].AuthorId, Is.EqualTo(1));
			Assert.That(books[3].AuthorId, Is.EqualTo(2));
			Assert.That(books[4].AuthorId, Is.EqualTo(3));
			Assert.That(books[5].AuthorId, Is.EqualTo(4));
			Assert.That(books[6].AuthorId, Is.EqualTo(4));

			Assert.That(books[0].Title, Is.EqualTo("Romeo and Juliet"));
			Assert.That(books[1].Title, Is.EqualTo("Hamlet"));
			Assert.That(books[2].Title, Is.EqualTo("Othello"));
			Assert.That(books[3].Title, Is.EqualTo("The Mysterious Affair at Styles"));
			Assert.That(books[4].Title, Is.EqualTo("The Lioness and the Lily"));
			Assert.That(books[5].Title, Is.EqualTo("Tycoon"));
			Assert.That(books[6].Title, Is.EqualTo("Piranhas"));

			Assert.That(books[0].YearPublished, Is.EqualTo(1597));
			Assert.That(books[1].YearPublished, Is.EqualTo(1600));
			Assert.That(books[2].YearPublished, Is.EqualTo(1603));
			Assert.That(books[3].YearPublished, Is.EqualTo(1916));
			Assert.That(books[4].YearPublished, Is.EqualTo(1841));
			Assert.That(books[5].YearPublished, Is.EqualTo(2011));
			Assert.That(books[6].YearPublished, Is.EqualTo(1992));
		}

		[Test]
		public void GetAllByAuthor_ShouldReturnBooksWrittenByTheGivenAuthor()
		{
			BooksRepository.ResetBooks();

			var books = _booksRepository.GetAllByAuthor(1).ToList();

			Assert.That(books, Is.Not.Null);
			Assert.That(books.Count(), Is.EqualTo(3));
			Assert.That(books[0].AuthorId, Is.EqualTo(1));
			Assert.That(books[1].AuthorId, Is.EqualTo(1));
			Assert.That(books[2].AuthorId, Is.EqualTo(1));
		}

		[Test]
		public void GetBookByAuthor_ShouldReturnNullWhenNoSuchBookWrittenByTheGivenAuthor()
		{
			BooksRepository.ResetBooks();

			var book = _booksRepository.GetBookByAuthor(1, 5);

			Assert.That(book, Is.Null);
		}

		[Test]
		public void GetBookByAuthor_ShouldReturnBookWrittenByTheGivenAuthor()
		{
			BooksRepository.ResetBooks();

			var book = _booksRepository.GetBookByAuthor(1, 3);

			Assert.That(book, Is.Not.Null);
			Assert.That(book.AuthorId, Is.EqualTo(1));
			Assert.That(book.Title, Is.EqualTo("Othello"));
			Assert.That(book.YearPublished, Is.EqualTo(1603));
		}
	}
}
