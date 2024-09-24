using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Service.Implementation;

namespace BootCamp2024.IntegrationTests.Authors
{
	public class AuthorsServiceTests
	{
		private readonly AuthorsService _authorsService;
		private readonly AuthorsRepository _authorsRepository;

		public AuthorsServiceTests()
		{
			_authorsRepository = new AuthorsRepository();
			_authorsService = new AuthorsService(_authorsRepository);
		}

		private void ResetDatabase()
		{
			AuthorsRepository.ResetAuthors();
		}

		[Fact]
		public void GetAll_ShouldReturnAllAuthors()
		{
			ResetDatabase();

			var authors = _authorsService.GetAll();

			Assert.NotEmpty(authors);
			Assert.Equal(4, authors.Count());
		}

		[Fact]
		public void GetById_ShouldReturnAuthor_WhenExists()
		{
			ResetDatabase();

			var existingAuthor = new Author
			{
				Id = 1,
				FirstName = "William",
				LastName = "Shakespeare",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"
			};

			var retrievedAuthor = _authorsService.GetById(existingAuthor.Id);

			Assert.NotNull(retrievedAuthor);
			Assert.Equal(existingAuthor.FirstName, retrievedAuthor.FirstName);
			Assert.Equal(existingAuthor.LastName, retrievedAuthor.LastName);
			Assert.Equal(existingAuthor.ImageUrl, retrievedAuthor.ImageUrl);
		}

		[Fact]
		public void CreateAuthor_ShouldAddAuthorToRepository()
		{
			ResetDatabase();

			var author = new Author 
			{ 
				Id = 5,
				FirstName = "Anne", 
				LastName = "Frank",
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
			};

			_authorsService.Create(author);

			var authors = _authorsService.GetAll();
			Assert.Contains(authors, a => 
				a.FirstName == "Anne" 
				&& a.LastName == "Frank"
				&& a.ImageUrl == "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF");
		}

		[Fact]
		public void Update_ShouldModifyExistingAuthor_WhenExists()
		{
			ResetDatabase();

			var existingAuthor = new Author
			{
				Id = 1,
				FirstName = "Agatha",
				LastName = "Christie",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png"
			};

			var updatedAuthor = new Author
			{
				Id = existingAuthor.Id,
				FirstName = "Aga",
				LastName = "Christy",
				ImageUrl = "https://example.com/new_image.jpg"
			};

			var updated = _authorsService.Update(updatedAuthor, existingAuthor.Id);

			Assert.True(updated);
			var retrievedAuthor = _authorsService.GetById(existingAuthor.Id);
			Assert.Equal(updatedAuthor.FirstName, retrievedAuthor.FirstName);
			Assert.Equal(updatedAuthor.LastName, retrievedAuthor.LastName);
			Assert.Equal(updatedAuthor.ImageUrl, retrievedAuthor.ImageUrl);
		}

		[Fact]
		public void Delete_ShouldRemoveAuthor_WhenExists()
		{
			ResetDatabase();

			var existingAuthor = new Author
			{
				Id = 3,
				FirstName = "Barbara",
				LastName = "Cartland",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/Dame_Barbara_Cartland_Allan_Warren.jpg/330px-Dame_Barbara_Cartland_Allan_Warren.jpg"
			};

			var deleted = _authorsService.Delete(existingAuthor.Id);

			Assert.True(deleted);
			var authors = _authorsService.GetAll();
			Assert.DoesNotContain(authors, a => a.Id == existingAuthor.Id);
		}
	}
}
