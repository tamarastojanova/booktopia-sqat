using BootCamp2024.Service.Interface;
using BootCamp2024.Domain.Models;
using BootCamp2024.Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootCamp2024.Api.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

		public AuthorsController(IAuthorsService authorsService)
		{
			_authorsService = authorsService;
		}

		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Author>))]
        public IActionResult Get()
        {
            var data = _authorsService.GetAll();
            if (!data.Any())
            {
                return Ok(new { Message = "No authors found." });
            }
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var data = _authorsService.GetById(id);
            if(data == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }
            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(Author author)
        {
            var existing = _authorsService.GetById(author.Id);
            if(existing != null)
            {
                return BadRequest(new { Message = "Author with the given id already exists in the database." });
            }
            try
            {
                author.Validate();
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
			_authorsService.Create(author);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, Author author)
        {
            if (id < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var existing = _authorsService.GetById(id);
            if (existing == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }
            try
            {
                author.Validate();
            }
            catch (Exception exception)
            {
                return BadRequest(new { exception.Message });
            }
            if(author.Id != id)
            {
                return BadRequest(new { Message = "You have sent different ids in the author object and in the id parameter." });
            }
			//author.Id = id;
			_authorsService.Update(author, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { Message = "Invalid ID. ID must be a positive integer." });
            }
            var existing = _authorsService.GetById(id);
            if (existing == null)
            {
                return NotFound(new { Message = $"Author with ID {id} not found." });
            }
			_authorsService.Delete(id);
            return Ok();
        }
    }
}
