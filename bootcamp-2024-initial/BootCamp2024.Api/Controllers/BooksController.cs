using BootCamp2024.Domain.Extensions;
using BootCamp2024.Domain.Models;
using BootCamp2024.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootCamp2024.Api.Controllers
{
    [ApiController]
    [Route("authors/{authorId}/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

		public BooksController(IBooksService booksService)
		{
			_booksService = booksService;
		}

		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Book>))]
        public IActionResult Get(int authorId)
        {
            var data = _booksService.GetAllByAuthor(authorId);
            if (!data.Any())
            {
                return Ok(new { Message = "No books found." });
            }
            return Ok(data);
        }

        [Route("{bookId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(int authorId, int bookId)
        {
            if (authorId < 0 || bookId < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var data = _booksService.GetBookByAuthor(authorId, bookId);
            if (data == null)
            {
                return NotFound(new { Message = $"Book with the given ID {bookId} from the author with ID {authorId} not found." });
            }
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(int authorId, Book book)
        {
            var existing = _booksService.GetById(book.Id);
            if(existing != null)
            {
                return BadRequest(new { Message = "Book with the given id already exists in the database." });
            }
            try
            {
                book.Validate();
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
            if(book.AuthorId != authorId)
            {
                return BadRequest(new { Message = "You have sent different ids in the book object and in the authorId parameter." });
            }
			//book.AuthorId = authorId;
			_booksService.Create(book);
            return Ok(book);
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int authorId, int bookId, Book book)
        {
            if (authorId < 0 || bookId < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var existing = _booksService.GetById(bookId);
            if (existing == null)
            {
                return NotFound(new { Message = $"Book with ID {bookId} not found." });
            }
            try
            {
                book.Validate();
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
            if(book.AuthorId != authorId)
            {
                return BadRequest(new { Message = "You have sent different ids in the book object and in the authorId parameter." });
            }
            if (book.Id != bookId)
            {
                return BadRequest(new { Message = "You have sent different ids in the book object and in the bookId parameter." });
            }
			//book.Id = bookId;
			//book.AuthorId = authorId;
			_booksService.Update(book, bookId);
            return Ok(book);
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int authorId, int bookId)
        {
            if (authorId < 0 || bookId < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var existing = _booksService.GetById(bookId);
            if (existing == null)
            {
                return NotFound(new { Message = $"Book with ID {bookId} not found." });
            }
            if(existing.AuthorId != authorId)
            {
                return BadRequest(new { Message = $"There is no such a book with id {bookId} from the author with id {authorId}" });
            }
			_booksService.Delete(bookId);
            return Ok(existing);
        }
    }
}
