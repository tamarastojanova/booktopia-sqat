using BootCamp2024.Api.Controllers;
using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BootCamp2024.UnitTests.Controllers
{
	[TestFixture]
	public class BooksControllerTests
	{
		private readonly BooksController _booksController;

		public BooksControllerTests()
		{
			var booksRepository = new BooksRepository();
			var booksService = new BooksService(booksRepository);
			_booksController = new BooksController(booksService);
		}

		[Test] // [1, 2, 3]
		public void GetMethod_ShouldReturnOk_WhenNoBooksFound()
		{
			BooksRepository.ResetBooks();

			var response = _booksController.Get(5);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo("No books found."));
		}

		[Test] // [1, 2, 4]
		public void GetMethod_ShouldReturnOk_WhenBooksExistForAuthor()
		{
			BooksRepository.ResetBooks();

			var response = _booksController.Get(1);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsInstanceOf<IEnumerable<Book>>(responseValue);

			var books = responseValue as IEnumerable<Book>;
			Assert.IsNotNull(books);
			Assert.IsNotEmpty(books);
			Assert.That(books.Count(), Is.EqualTo(3));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6]
		[TestCase(-2, -3, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(1, 15, typeof(NotFoundObjectResult), "Book with the given ID 15 from the author with ID 1 not found.")]
		public void GetByIdMethod_ShouldReturnExpectedResponse_WhenIdIsInvalid(int authorId, int bookId, Type expectedResponseType, string expectedMessage)
		{
			BooksRepository.ResetBooks();

			var response = _booksController.Get(authorId, bookId);

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7]
		public void GetByIdMethod_ShouldReturnOk_WhenBookExists()
		{
			BooksRepository.ResetBooks();

			var response = _booksController.Get(1, 1);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var retrievedBook = responseValue as Book;
			Assert.IsNotNull(retrievedBook);
			Assert.That(retrievedBook.Id, Is.EqualTo(1));
			Assert.That(retrievedBook.AuthorId, Is.EqualTo(1));
			Assert.That(retrievedBook.Title, Is.EqualTo("Romeo and Juliet"));
			Assert.That(retrievedBook.YearPublished, Is.EqualTo(1597));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6], [1, 2, 4, 7, 8]
		[TestCase(1, 1, "Macbeth", 1600, 1, typeof(BadRequestObjectResult), "Book with the given id already exists in the database.")]
		[TestCase(8, 1, "Macbeth", 2025, 1, typeof(BadRequestObjectResult), "Year published should be less than or equal to the current year")]
		[TestCase(8, 2, "Macbeth", 1600, 1, typeof(BadRequestObjectResult), "You have sent different ids in the book object and in the authorId parameter.")]
		public void PostMethod_ShouldReturnBadRequest(int bookId, int authorId, string title, int yearPublished, int bookAuthorId, Type expectedResponseType, string expectedMessage)
		{
			BooksRepository.ResetBooks();

			Book book = new Book
			{
				Id = bookId,
				AuthorId = bookAuthorId,
				Title = title,
				YearPublished = yearPublished
			};

			var response = _booksController.Post(authorId, book);

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 7, 9, 10]
		public void PostMethod_ShouldReturnOk_WhenBookIsValid()
		{
			BooksRepository.ResetBooks();

			Book book = new Book
			{
				Id = 8,
				AuthorId = 1,
				Title = "Macbeth",
				YearPublished = 1600
			};

			var response = _booksController.Post(1, book);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var createdBook = responseValue as Book;
			Assert.IsNotNull(createdBook);
			Assert.That(createdBook.Id, Is.EqualTo(book.Id));
			Assert.That(createdBook.Title, Is.EqualTo(book.Title));
			Assert.That(createdBook.YearPublished, Is.EqualTo(book.YearPublished));
			Assert.That(createdBook.AuthorId, Is.EqualTo(book.AuthorId));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6]
		[TestCase(-5, -7, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(1, 8, typeof(NotFoundObjectResult), "Book with ID 8 not found.")]
		public void PutMethod_ShouldReturnExpectedResponse_WhenIdIsInvalid(int authorId, int bookId, Type expectedResponseType, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			var response = _booksController.Put(authorId, bookId, new Book());

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[TestCase(1, 1, "My Grandmother Asked Me to Tell You She's Sorry", 1600, 1, "Book must have a title and it should be less than 25 characters")] // Test case for invalid title
		[TestCase(1, 2, "Macbeth", 1600, 1, "You have sent different ids in the book object and in the bookId parameter.")] // Test case for different book IDs
		[TestCase(2, 1, "Macbeth", 1600, 1, "You have sent different ids in the book object and in the authorId parameter.")] // Test case for different author IDs
		public void PutMethod_ShouldReturnExpectedBadRequest_WhenBookIsInvalid(int authorId, int bookId, string title, int yearPublished, int bookAuthorId, string expectedMessage)
		{
			BooksRepository.ResetBooks();

			Book book = new Book
			{
				Id = 1,
				AuthorId = bookAuthorId,
				Title = title,
				YearPublished = yearPublished
			};

			var response = _booksController.Put(authorId, bookId, book);

			Assert.IsInstanceOf<BadRequestObjectResult>(response);
			var badRequestResult = response as BadRequestObjectResult;
			Assert.IsNotNull(badRequestResult);
			var responseValue = badRequestResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7, 10, 12, 14, 15]
		public void PutMethod_ShouldReturnOk_WhenBookIsValid()
		{
			BooksRepository.ResetBooks();

			Book book = new Book
			{
				Id = 1,
				AuthorId = 1,
				Title = "Macbeth",
				YearPublished = 1600
			};

			var response = _booksController.Put(1, 1, book);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var updatedBook = responseValue as Book;
			Assert.IsNotNull(updatedBook);
			Assert.That(updatedBook.Title, Is.EqualTo(book.Title));
			Assert.That(updatedBook.YearPublished, Is.EqualTo(book.YearPublished));
			Assert.That(updatedBook.AuthorId, Is.EqualTo(book.AuthorId));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6], [1, 2, 4, 5, 7, 8]
		[TestCase(-1, -9, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(1, 9, typeof(NotFoundObjectResult), "Book with ID 9 not found.")]
		[TestCase(1, 4, typeof(BadRequestObjectResult), "There is no such a book with id 4 from the author with id 1")]
		public void DeleteMethod_ShouldReturnExpectedResponse_WhenIdsAreInvalid(int authorId, int bookId, Type expectedResponseType, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			var response = _booksController.Delete(authorId, bookId);

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7, 9, 10]
		public void Delete_Test4()
		{
			BooksRepository.ResetBooks();

			var response = _booksController.Delete(1, 1);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);
		}
	}
}
