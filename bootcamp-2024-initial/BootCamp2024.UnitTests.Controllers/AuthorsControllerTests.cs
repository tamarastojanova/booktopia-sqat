using BootCamp2024.Api.Controllers;
using BootCamp2024.Domain.Extensions;
using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BootCamp2024.UnitTests.Controllers
{
	[TestFixture]
	public class AuthorsControllerTests
	{
		private readonly AuthorsController _authorsController;

		public AuthorsControllerTests()
		{
			var authorsRepository = new AuthorsRepository();
			var authorsService = new AuthorsService(authorsRepository);
			_authorsController = new AuthorsController(authorsService);
		}

		[Test] // [1, 2, 3]
		public void GetMethod_ShouldReturnOk_WhenNoAuthorsFound()
		{
			var authorsList = new List<Author>();
			AuthorsRepository.SetAuthors(authorsList);

			var response = _authorsController.Get();

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);

			Assert.That(responseDictionary["Message"], Is.EqualTo("No authors found."));
		}

		[Test] // [1, 2, 4]
		public void GetMethod_ShouldReturnOk_WhenAuthorsExist()
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Get();

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var authorsResponse = responseValue as IEnumerable<Author>;
			Assert.IsNotNull(authorsResponse);
			Assert.That(authorsResponse.Count(), Is.EqualTo(4));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6]
		[TestCase(-5, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(10, typeof(NotFoundObjectResult), "Author with ID 10 not found.")]
		public void GetByIdMethod_ShouldReturnExpectedResponse_WhenIdIsInvalid(int id, Type expectedResponseType, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Get(id);

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);

			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7]
		public void GetByIdMethod_ShouldReturnOk_WhenAuthorExists()
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Get(1);
			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var author = responseValue as Author;
			Assert.IsNotNull(author);
			Assert.That(author.Id, Is.EqualTo(1));
			Assert.That(author.FirstName, Is.EqualTo("William"));
			Assert.That(author.LastName, Is.EqualTo("Shakespeare"));
			Assert.That(author.ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"));
		}

		[TestCase(1, "William", "Shakespeare", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg", "Author with the given id already exists in the database.")] 
		[TestCase(5, "Christopher", "Shakespeare", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg", "Author name should be between 3 and 10 characters")] 
		public void PostMethod_ShouldReturnExpectedResponse_WhenPostingAuthor(int id, string firstName, string lastName, string imageUrl, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				Id = id,
				FirstName = firstName,
				LastName = lastName,
				ImageUrl = imageUrl
			};

			var response = _authorsController.Post(author);

			Assert.IsInstanceOf<BadRequestObjectResult>(response);
			var badRequestResult = response as BadRequestObjectResult;
			Assert.IsNotNull(badRequestResult);
			var responseValue = badRequestResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 7, 8]
		public void PostMethod_ShouldReturnOk_WhenAuthorIsValid()
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				Id = 5,
				FirstName = "Anne",
				LastName = "Frank",
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
			};

			var response = _authorsController.Post(author);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value as Author;
			Assert.IsNotNull(responseValue);
			Assert.That(responseValue.Id, Is.EqualTo(author.Id));
			Assert.That(responseValue.FirstName, Is.EqualTo(author.FirstName));
			Assert.That(responseValue.LastName, Is.EqualTo(author.LastName));
			Assert.That(responseValue.ImageUrl, Is.EqualTo(author.ImageUrl));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6]
		[TestCase(-6, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(5, typeof(NotFoundObjectResult), "Author with ID 5 not found.")]
		public void PutMethod_ShouldReturnExpectedResponse_WhenIdIsInvalid(int id, Type expectedResponseType, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Put(id, new Author());

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[TestCase(1, 1, "Christopher", "Shakespeare", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg", "Author name should be between 3 and 10 characters")]
		[TestCase(1, 2, "Will", "Shakespeare", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg", "You have sent different ids in the author object and in the id parameter.")]
		public void PutMethod_ShouldReturnExpectedResponse_WhenUpdatingAuthor(int id, int authorId, string firstName, string lastName, string imageUrl, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				Id = authorId,
				FirstName = firstName,
				LastName = lastName,
				ImageUrl = imageUrl
			};

			var response = _authorsController.Put(id, author);

			Assert.IsInstanceOf<BadRequestObjectResult>(response);
			var badRequestResult = response as BadRequestObjectResult;
			Assert.IsNotNull(badRequestResult);
			var responseValue = badRequestResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7, 10, 12, 13]
		public void Put_ShouldReturnOk_WhenAuthorIsUpdatedSuccessfully()
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				Id = 1,
				FirstName = "Will",
				LastName = "Shakespeare",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"
			};

			var response = _authorsController.Put(1, author);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);

			var updatedAuthor = responseValue as Author;
			Assert.IsNotNull(updatedAuthor);
			Assert.That(updatedAuthor.FirstName, Is.EqualTo(author.FirstName));
			Assert.That(updatedAuthor.LastName, Is.EqualTo(author.LastName));
			Assert.That(updatedAuthor.ImageUrl, Is.EqualTo(author.ImageUrl));
		}

		[Test] // [1, 2, 3], [1, 2, 4, 5, 6]
		[TestCase(-3, typeof(BadRequestObjectResult), "Invalid ID. ID must be a positive integer.")]
		[TestCase(5, typeof(NotFoundObjectResult), "Author with ID 5 not found.")]
		public void Delete_ShouldReturnExpectedResponse_WhenIdIsInvalidOrNotFound(int id, Type expectedResponseType, string expectedMessage)
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Delete(id);

			Assert.IsInstanceOf(expectedResponseType, response);
			var objectResult = response as ObjectResult;
			Assert.IsNotNull(objectResult);
			var responseValue = objectResult.Value;
			Assert.IsNotNull(responseValue);

			var responseDictionary = JsonHelperClass.JsonSerializeAndDeserialize(responseValue);
			Assert.That(responseDictionary["Message"], Is.EqualTo(expectedMessage));
		}

		[Test] // [1, 2, 4, 5, 7, 8]
		public void Delete_ShouldReturnOk_WhenAuthorIsDeletedSuccessfully()
		{
			AuthorsRepository.ResetAuthors();

			var response = _authorsController.Delete(1);

			Assert.IsInstanceOf<OkObjectResult>(response);
			var okResult = response as OkObjectResult;
			Assert.IsNotNull(okResult);
			var responseValue = okResult.Value;
			Assert.IsNotNull(responseValue);
		}
	}
}

