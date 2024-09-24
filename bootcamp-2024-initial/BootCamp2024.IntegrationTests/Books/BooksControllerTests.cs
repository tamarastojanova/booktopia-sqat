using BootCamp2024.Domain.Models;
using System.Net.Http.Json;
using BootCamp2024.Api;
using FluentAssertions;
using BootCamp2024.Repository.Repositories.Implementation;

namespace BootCamp2024.IntegrationTests.Books
{
    public class BooksControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public BooksControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClientWithBaseAddress();
        }

        private void ResetDatabase()
        {
            BooksRepository.ResetBooks();
        }

        [Fact]
        public async Task Get_ShouldReturnBooksList()
        {
            ResetDatabase();

            var authorId = 1;

            var response = await _client.GetAsync($"{authorId}/books");

            response.EnsureSuccessStatusCode();
            var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
            books.Should().NotBeEmpty();
            books.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetById_ShouldReturnBook_WhenIdsAreValid()
        {
            ResetDatabase();

            var expectedBook = new Book
            {
                Id = 2,
                AuthorId = 1,
                Title = "Hamlet",
                YearPublished = 1600
            };

            var response = await _client.GetAsync($"{expectedBook.AuthorId}/books/{expectedBook.Id}");

            response.EnsureSuccessStatusCode();
            var book = await response.Content.ReadFromJsonAsync<Book>();
            book.Should().NotBeNull();
            book.Id.Should().Be(expectedBook.Id);
            book.AuthorId.Should().Be(expectedBook.AuthorId);
            book.Title.Should().Be(expectedBook.Title);
            book.YearPublished.Should().Be(expectedBook.YearPublished);
        }

        [Fact]
        public async Task GetById_ShouldReturnBadRequest_WhenIdsAreInvalid()
        {
            ResetDatabase();

            var invalidBookId = -1;
            var invalidAuthorId = -1;

            var response = await _client.GetAsync($"{invalidAuthorId}/books/{invalidBookId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenAuthorAndBookWithIdDoNotExist()
        {
            ResetDatabase();

            var nonExistentAuthorId = 999;
            var nonExistentBookId = 999;

            var response = await _client.GetAsync($"{nonExistentAuthorId}/books/{nonExistentBookId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ShouldAddNewBook()
        {
            ResetDatabase();

            var newBook = new Book
            {
                Id = 8,
                AuthorId = 1,
                Title = "Macbeth",
                YearPublished = 1600
            };

            var response = await _client.PostAsJsonAsync($"{newBook.AuthorId}/books", newBook);

            response.EnsureSuccessStatusCode();
            var book = await response.Content.ReadFromJsonAsync<Book>();
            book.Should().NotBeNull();
            book.Id.Should().Be(newBook.Id);
            book.AuthorId.Should().Be(newBook.AuthorId);
            book.Title.Should().Be(newBook.Title);
            book.YearPublished.Should().Be(newBook.YearPublished);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenIdAlreadyExists()
        {
            ResetDatabase();

            var newBook = new Book
            {
                Id = 2,
                AuthorId = 1,
                Title = "Hamlet",
                YearPublished = 1600
            };

            var response = await _client.PostAsJsonAsync($"{newBook.AuthorId}/books", newBook);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenDifferentIdsAreSent()
        {
            ResetDatabase();

            var newBook = new Book
            {
                Id = 8,
                AuthorId = 1,
                Title = "Macbeth",
                YearPublished = 1600
            };

            var response = await _client.PostAsJsonAsync("2/books", newBook);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldModifyExistingBook()
        {
            ResetDatabase();

            var updatedBook = new Book
            {
                Id = 3,
                AuthorId = 1,
                Title = "Julius Caesar",
                YearPublished = 1599
            };

            var response = await _client.PutAsJsonAsync($"{updatedBook.AuthorId}/books/{updatedBook.Id}", updatedBook);

            response.EnsureSuccessStatusCode();
            var book = await response.Content.ReadFromJsonAsync<Book>();
            book.Should().NotBeNull();
            book.Id.Should().Be(updatedBook.Id);
            book.AuthorId.Should().Be(updatedBook.AuthorId);
            book.Title.Should().Be(updatedBook.Title);
            book.YearPublished.Should().Be(updatedBook.YearPublished);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdsAreInvalid()
        {
            ResetDatabase();

            var updatedBook = new Book
            {
                Id = -1,
                AuthorId = -1,
                Title = "Julius Caesar",
                YearPublished = 1599
            };

            var response = await _client.PutAsJsonAsync($"{updatedBook.AuthorId}/books/{updatedBook.Id}", updatedBook);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenAuthorAndBookWithIdDoNotExist()
        {
            ResetDatabase();

            var updatedBook = new Book
            {
                Id = 999,
                AuthorId = 999,
                Title = "Julius Caesar",
                YearPublished = 1599
            };

            var response = await _client.PutAsJsonAsync($"{updatedBook.AuthorId}/books/{updatedBook.Id}", updatedBook);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenDifferentIdsAreSent()
        {
            ResetDatabase();

            var updatedBook = new Book
            {
                Id = 2,
                AuthorId = 1,
                Title = "Julius Caesar",
                YearPublished = 1599
            };

            var response = await _client.PutAsJsonAsync($"3/books/5", updatedBook);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldRemoveBook()
        {
            ResetDatabase();

            var validBookId = 1;
            var validAuthorId = 1;

            var response = await _client.DeleteAsync($"{validAuthorId}/books/{validBookId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            ResetDatabase();

            var invalidBookId = -1;
            var invalidAuthorId = -1;

            var response = await _client.DeleteAsync($"{invalidAuthorId}/books/{invalidBookId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenAuthorAndBookWithIdDoNotExist()
        {
            ResetDatabase();

            var nonExistentBookId = 999;
            var nonExistentAuthorId = 999;

            var response = await _client.DeleteAsync($"{nonExistentAuthorId}/books/{nonExistentBookId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
