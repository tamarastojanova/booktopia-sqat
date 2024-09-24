using BootCamp2024.Domain.Extensions;
using BootCamp2024.Domain.Models;

namespace BootCamp2024.UnitTests.Extensions
{
	[TestFixture]
	public class BookValidatorTests
	{
		[Test] // if (book != null)
			   // ;
		public void ShouldThrowArgumentNullException_WhenBookIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => BookValidator.Validate(null));
		}

		[Test] // if (string.IsNullOrEmpty(book.Title) && book.Title.Length > 25)
			   // ;
			   // throw new ArgumentException("")
		public void ShouldThrowArgumentException_WhenTitleIsEmpty()
		{
			var book = new Book { Title = "", YearPublished = 1600, AuthorId = 1 };
			Assert.That(Assert.Throws<ArgumentException>(() => BookValidator.Validate(book)).Message, Is.EqualTo("Book must have a title and it should be less than 25 characters"));
		}

		[Test] // if (!(string.IsNullOrEmpty(book.Title) || book.Title.Length > 25))
			   // if ((book.Title != null) || book.Title.Length > 25)
			   // if ((book.Title != "") || book.Title.Length > 25)
			   // if (string.IsNullOrEmpty(book.Title) || book.Title.Length < 25)
			   // if(!(book.YearPublished > DateTime.Now.Year || book.YearPublished == null))
			   // if(book.YearPublished < DateTime.Now.Year || book.YearPublished == null)
			   // if(book.YearPublished > DateTime.Now.Year || book.YearPublished != null)
			   // if(book.AuthorId != null)
		public void ShouldNotThrowArgumentException_WhenBookTitleIsValid()
		{
			var book = new Book { Title = "Macbeth", YearPublished = 1600, AuthorId = 1 };
			Assert.DoesNotThrow(() => BookValidator.Validate(book));
		}

		[Test] // if (string.IsNullOrEmpty(book.Title) || book.Title.Length >= 25)
			   // if(book.YearPublished >= DateTime.Now.Year || book.YearPublished == null)
		public void ShouldNotThrowArgumentException_ForBookTitleWithTwentyFiveLettersPublishedThisYear()
		{
		    var book = new Book { Title = "Journey Through The Woods", YearPublished = 2024, AuthorId = 1 };
			Assert.DoesNotThrow(() => BookValidator.Validate(book));
		}

		[Test] // if(book.YearPublished > DateTime.Now.Year && book.YearPublished == null)
			   // ;
			   // throw new ArgumentException("")
		public void ShouldThrowArgumentException_ForBookPublishedInTheFuture()
		{
			var book = new Book { Title = "Macbeth", YearPublished = 2025, AuthorId = 1 };
			Assert.That(Assert.Throws<ArgumentException>(() => BookValidator.Validate(book)).Message, Is.EqualTo("Year published should be less than or equal to the current year"));
		}

		[Test] // ;
			   // throw new ArgumentException("")
		public void ShouldThrowArgumentException_WhenBookDoesNotHaveAuthorId()
		{
			var book = new Book { Title = "Macbeth", YearPublished = 1600 };
			Assert.That(Assert.Throws<ArgumentException>(() => BookValidator.Validate(book)).Message, Is.EqualTo("Book must have an author"));
		}
	}
}
