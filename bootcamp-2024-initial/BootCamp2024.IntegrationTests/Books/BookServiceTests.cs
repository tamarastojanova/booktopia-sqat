using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Service.Implementation;

namespace BootCamp2024.IntegrationTests.Books
{
	public class BooksServiceTests
	{
		private readonly BooksService _booksService;
		private readonly BooksRepository _booksRepository;

		public BooksServiceTests()
		{
			_booksRepository = new BooksRepository();
			_booksService = new BooksService(_booksRepository);
		}

		private void ResetDatabase()
		{
			BooksRepository.ResetBooks();
		}

		[Fact]
		public void GetAll_ShouldReturnAllBooks()
		{
			ResetDatabase();

			var books = _booksService.GetAll();

			Assert.NotEmpty(books);
			Assert.Equal(7, books.Count());
		}


		[Fact]
		public void GetById_ShouldReturnBook_WhenExists()
		{
			ResetDatabase();

			var existingBook = new Book
			{
				Id = 3,
				AuthorId = 1,
				Title = "Othello",
				YearPublished = 1603
			};

			var retrievedBook = _booksService.GetById(existingBook.Id);

			Assert.NotNull(retrievedBook);
			Assert.Equal(existingBook.Title, retrievedBook.Title);
			Assert.Equal(existingBook.YearPublished, retrievedBook.YearPublished);
			Assert.Equal(existingBook.AuthorId, retrievedBook.AuthorId);
		}

		[Fact]
		public void GetAllByAuthor_ShouldReturnBooksForGivenAuthor()
		{
			ResetDatabase();

			var authorId = 1;
			var booksByAuthor = _booksService.GetAllByAuthor(authorId);

			Assert.NotEmpty(booksByAuthor);
			Assert.Equal(3, booksByAuthor.Count());
			Assert.All(booksByAuthor, b => Assert.Equal(authorId, b.AuthorId));
		}

		[Fact]
		public void GetBookByAuthor_ShouldReturnCorrectBook_WhenExists()
		{
			ResetDatabase();

			var existingBook = new Book
			{
				Id = 2,
				AuthorId = 1,
				Title = "Hamlet",
				YearPublished = 1600
			};
			int authorId = 1;

			var book = _booksService.GetBookByAuthor(authorId, existingBook.Id);

			Assert.NotNull(book);
			Assert.Equal(authorId, book.AuthorId);
			Assert.Equal(existingBook.Id, book.Id);
			Assert.Equal(existingBook.Title, book.Title);
			Assert.Equal(existingBook.YearPublished, book.YearPublished);
		}


		[Fact]
		public void CreateBook_ShouldAddBookToRepository()
		{
			ResetDatabase();

			var book = new Book
			{
				Id = 8,
				AuthorId = 1,
				Title = "Macbeth",
				YearPublished = 1600
			};

			_booksService.Create(book);

			var books = _booksService.GetAll();
			Assert.Contains(books, b =>
				b.Title == "Macbeth"
				&& b.YearPublished == 1600
				&& b.AuthorId == 1);
		}

		[Fact]
		public void Update_ShouldModifyExistingBook_WhenExists()
		{
			ResetDatabase();

			var existingBook = new Book
			{
				Id = 1,
				AuthorId = 1,
				Title = "Romeo and Juliet",
				YearPublished = 1597
			};

			var updatedBook = new Book
			{
				Id = existingBook.Id,
				AuthorId = 1,
				Title = "Macbeth",
				YearPublished = 1600
			};

			var updated = _booksService.Update(updatedBook, existingBook.Id);

			Assert.True(updated);
			var retrievedBook = _booksService.GetById(existingBook.Id);
			Assert.Equal("Macbeth", retrievedBook.Title);
			Assert.Equal(1600, retrievedBook.YearPublished);
		}

		[Fact]
		public void Delete_ShouldRemoveBook_WhenExists()
		{
			ResetDatabase();

			var existingBook = new Book
			{
				Id = 1,
				AuthorId = 1,
				Title = "Romeo and Juliet",
				YearPublished = 1597
			};

			var deleted = _booksService.Delete(existingBook.Id);

			Assert.True(deleted);
			var books = _booksService.GetAll();
			Assert.DoesNotContain(books, b => b.Id == existingBook.Id);
		}
	}
}
