using BootCamp2024.Domain.Models;
using System.Net.Http.Json;
using BootCamp2024.Api;
using FluentAssertions;
using BootCamp2024.Repository.Repositories.Implementation;

namespace BootCamp2024.IntegrationTests.Authors
{
    public class AuthorsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthorsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClientWithBaseAddress();
        }

        private void ResetDatabase()
        {
            AuthorsRepository.ResetAuthors();
        }

        [Fact]
        public async Task Get_ShouldReturnAuthorsList()
        {
            ResetDatabase();

            var response = await _client.GetAsync("");

            response.EnsureSuccessStatusCode();
            var authors = await response.Content.ReadFromJsonAsync<IEnumerable<Author>>();
            authors.Should().NotBeEmpty();
            authors.Should().HaveCount(4);
        }

        [Fact]
        public async Task GetById_ShouldReturnAuthor_WhenIdIsValid()
        {
            ResetDatabase();

            var expectedAuthor = new Author
            {
                Id = 1,
                FirstName = "William",
                LastName = "Shakespeare",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"
            };

            var response = await _client.GetAsync($"{expectedAuthor.Id}");

            response.EnsureSuccessStatusCode();
            var author = await response.Content.ReadFromJsonAsync<Author>();
            author.Should().NotBeNull();
            author.Id.Should().Be(expectedAuthor.Id);
            author.FirstName.Should().Be(expectedAuthor.FirstName);
            author.LastName.Should().Be(expectedAuthor.LastName);
            author.ImageUrl.Should().Be(expectedAuthor.ImageUrl);
        }

        [Fact]
        public async Task GetById_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            ResetDatabase();

            var invalidId = -1;

            var response = await _client.GetAsync($"{invalidId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenAuthorWithIdDoesNotExist()
        {
            ResetDatabase();

            var nonExistentId = 999;

            var response = await _client.GetAsync($"{nonExistentId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ShouldAddNewAuthor()
        {
            ResetDatabase();

            var newAuthor = new Author
            {
                Id = 5,
                FirstName = "Anne",
                LastName = "Frank",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9l1-hztg7UN_-ReH1FpfNDHImcEukolgPxvmlYN2jDQXTy0dF"
            };

            var response = await _client.PostAsJsonAsync("", newAuthor);

            response.EnsureSuccessStatusCode();
            var author = await response.Content.ReadFromJsonAsync<Author>();
            author.Should().NotBeNull();
            author.Id.Should().Be(newAuthor.Id);
            author.FirstName.Should().Be(newAuthor.FirstName);
            author.LastName.Should().Be(newAuthor.LastName);
            author.ImageUrl.Should().Be(newAuthor.ImageUrl);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenIdAlreadyExists()
        {
            ResetDatabase();

            var newAuthor = new Author
            {
                Id = 1,
                FirstName = "William",
                LastName = "Shakespeare",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg"
            };

            var response = await _client.PostAsJsonAsync("", newAuthor);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldModifyExistingAuthor()
        {
            ResetDatabase();

            var updatedAuthor = new Author
            {
                Id = 4,
                FirstName = "George",
                LastName = "Orwell",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s"
            };

            var response = await _client.PutAsJsonAsync($"{updatedAuthor.Id}", updatedAuthor);

            response.EnsureSuccessStatusCode();
            var author = await response.Content.ReadFromJsonAsync<Author>();
            author.Should().NotBeNull();
            author.Id.Should().Be(updatedAuthor.Id);
            author.FirstName.Should().Be(updatedAuthor.FirstName);
            author.LastName.Should().Be(updatedAuthor.LastName);
            author.ImageUrl.Should().Be(updatedAuthor.ImageUrl);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            ResetDatabase();

            var updatedAuthor = new Author
            {
                Id = -1,
                FirstName = "George",
                LastName = "Orwell",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s"
            };

            var response = await _client.PutAsJsonAsync($"{updatedAuthor.Id}", updatedAuthor);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenAuthorWithIdDoesNotExist()
        {
            ResetDatabase();

            var updatedAuthor = new Author
            {
                Id = 999,
                FirstName = "George",
                LastName = "Orwell",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s"
            };

            var response = await _client.PutAsJsonAsync($"{updatedAuthor.Id}", updatedAuthor);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenDifferentIdsAreSent()
        {
            ResetDatabase();

            var updatedAuthor = new Author
            {
                Id = 5,
                FirstName = "George",
                LastName = "Orwell",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8cwItnEGlr95H3OsMQJgJQBjrb-DKRLTrLQ&s"
            };

            var response = await _client.PutAsJsonAsync($"1", updatedAuthor);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldRemoveAuthor()
        {
            ResetDatabase();

            var validId = 4;

            var response = await _client.DeleteAsync($"{validId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            ResetDatabase();

            var invalidId = -1;

            var response = await _client.DeleteAsync($"{invalidId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenAuthorWithIdDoesNotExist()
        {
            ResetDatabase();

            var nonExistentId = 999;

            var response = await _client.DeleteAsync($"{nonExistentId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
