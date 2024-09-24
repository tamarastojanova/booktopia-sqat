using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Implementation;
using BootCamp2024.Repository.Repositories.Interface;

namespace BootCamp2024.UnitTests.Repositories
{
	[TestFixture]
	public class AuthorRepositoryTests
	{
		private readonly IAuthorsRepository _authorsRepository;

		public AuthorRepositoryTests()
		{
			_authorsRepository = new AuthorsRepository();
		}

		[Test]
		public void GetAll_ShouldReturnAuthors_ListMutant()
		{
			AuthorsRepository.ResetAuthors();

			var authors = _authorsRepository.GetAll().ToList();

			Assert.That(authors, Is.Not.Null);
			Assert.That(authors.Count(), Is.EqualTo(4));

			Assert.That(authors[0].FirstName, Is.EqualTo("William"));
			Assert.That(authors[1].FirstName, Is.EqualTo("Agatha"));
			Assert.That(authors[2].FirstName, Is.EqualTo("Barbara"));
			Assert.That(authors[3].FirstName, Is.EqualTo("Harold"));
			Assert.That(authors[0].LastName, Is.EqualTo("Shakespeare"));
			Assert.That(authors[1].LastName, Is.EqualTo("Christie"));
			Assert.That(authors[2].LastName, Is.EqualTo("Cartland"));
			Assert.That(authors[3].LastName, Is.EqualTo("Robbins"));
			Assert.That(authors[0].ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"));
			Assert.That(authors[1].ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png"));
			Assert.That(authors[2].ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/Dame_Barbara_Cartland_Allan_Warren.jpg/330px-Dame_Barbara_Cartland_Allan_Warren.jpg"));
			Assert.That(authors[3].ImageUrl, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Harold_Robbins_%281979%29.jpg/330px-Harold_Robbins_%281979%29.jpg"));
		}

		[Test]
		public void GetAll_ShouldReturnAuthors()
		{
			AuthorsRepository.ResetAuthors();

			var authors = _authorsRepository.GetAll();

			Assert.That(authors, Is.Not.Null);
			Assert.That(authors.Count(), Is.EqualTo(4));
		}

		[Test]
		public void Create_ShouldAddAuthor_WhenListIsEmpty()
		{
			AuthorsRepository.SetAuthors(new List<Author>());

			Author author = new Author
			{
				FirstName = "Anne",
				LastName = "Frank",
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
			};

			_authorsRepository.Create(author);

			var authors = _authorsRepository.GetAll().ToList();

			Assert.That(authors, Is.Not.Null);
			Assert.That(authors.Count(), Is.EqualTo(1));
			Assert.That(authors[0].Id, Is.EqualTo(1));
			Assert.That(authors[0].FirstName, Is.EqualTo("Anne"));
			Assert.That(authors[0].LastName, Is.EqualTo("Frank"));
		}

		[Test]
		public void Create_ShouldAddAuthor_WhenListIsNotEmpty()
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				FirstName = "Anne",
				LastName = "Frank",
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
			};

			_authorsRepository.Create(author);

			var authors = _authorsRepository.GetAll().ToList();

			Assert.That(authors, Is.Not.Null);
			Assert.That(authors.Count(), Is.EqualTo(5));
			Assert.That(authors[4].Id, Is.EqualTo(5));
			Assert.That(authors[4].FirstName, Is.EqualTo("Anne"));
			Assert.That(authors[4].LastName, Is.EqualTo("Frank"));

			var existingIds = authors.Select(a => a.Id).ToList();
			Assert.That(existingIds, Is.Unique);
		}


		[Test]
		public void Delete_ShouldDeleteAuthor_WhenIdIsInvalid()
		{
			AuthorsRepository.ResetAuthors();

			var isDeleted = _authorsRepository.Delete(10);

			Assert.That(isDeleted, Is.False);
		}

		[Test]
		public void Delete_ShouldDeleteAuthor_WhenIdIsValid()
		{
			AuthorsRepository.ResetAuthors();

			var isDeleted = _authorsRepository.Delete(1);

			Assert.That(isDeleted, Is.True);

			var authors = _authorsRepository.GetAll().ToList();

			Assert.That(authors, Is.Not.Null);
			Assert.That(authors.Count(), Is.EqualTo(3));
			Assert.That(authors[0].FirstName, Is.Not.EqualTo("William"));
			Assert.That(authors[0].FirstName, Is.Not.EqualTo("Shakespeare"));
		}

		[Test]
		public void GetById_ShouldReturnNull_WhenIdIsInvalid()
		{
			AuthorsRepository.ResetAuthors();

			var author = _authorsRepository.GetById(10);

			Assert.That(author, Is.Null);
		}

		[Test]
		public void GetById_ShouldRetrieveAuthor_WhenIdIsValid()
		{
			AuthorsRepository.ResetAuthors();

			var author = _authorsRepository.GetById(1);

			Assert.That(author, Is.Not.Null);
			Assert.That(author.FirstName, Is.EqualTo("William"));
			Assert.That(author.LastName, Is.EqualTo("Shakespeare"));
		}

		[Test]
		public void Update_ShouldBeSuccessfull_WhenIdIsValid()
		{
			AuthorsRepository.ResetAuthors();

			Author author = new Author
			{
				Id = 1,
				FirstName = "Anne",
				LastName = "Frank",
				ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
			};

			var isUpdated = _authorsRepository.Update(author, 1);

			Assert.That(isUpdated, Is.True);

			var updatedAuthor = _authorsRepository.GetById(1);

			Assert.That(updatedAuthor, Is.Not.Null);
			Assert.That(updatedAuthor.FirstName, Is.EqualTo("Anne"));
			Assert.That(updatedAuthor.LastName, Is.EqualTo("Frank"));
		}
	}
}
