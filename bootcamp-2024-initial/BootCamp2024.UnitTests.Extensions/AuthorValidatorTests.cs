using BootCamp2024.Domain.Extensions;
using BootCamp2024.Domain.Models;

namespace BootCamp2024.UnitTests.Extensions
{
	[TestFixture]
	public class AuthorValidatorTests
	{
		[Test] // if(author != null)
			   // ;
		public void ShouldThrowArgumentNullException_WhenAuthorIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => AuthorValidator.Validate(null));
		}

		[Test] // if(string.IsNullOrEmpty(author.FirstName) && string.IsNullOrEmpty(author.LastName))
			   // ;
		       // throw new ArgumentException("")
		public void ShouldThrowArgumentException_WhenLastNameIsEmpty()
		{
			var author = new Author { FirstName = "William", LastName = "" };
			Assert.That(Assert.Throws<ArgumentException>(() => AuthorValidator.Validate(author)).Message, Is.EqualTo("Author must have a first and last name"));
		}

		[Test] // if(!(string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
			   // if((author.FirstName != null) || string.IsNullOrEmpty(author.LastName))
			   // if((author.FirstName != "") || string.IsNullOrEmpty(author.LastName))
			   // if(string.IsNullOrEmpty(author.FirstName) || (author.LastName != null))
			   // if(string.IsNullOrEmpty(author.FirstName) || (author.LastName != ""))
			   // if(!(author.FirstName.Length<3 || author.FirstName.Length>10))
			   // if(author.FirstName.Length>3 || author.FirstName.Length>10)
			   // if(author.FirstName.Length<3 || author.FirstName.Length<10)
			   // if (!(author.LastName.Length< 3 || author.LastName.Length> 20))
			   // if (author.LastName.Length > 3 || author.LastName.Length > 20)
			   // if (author.LastName.Length< 3 || author.LastName.Length< 20)
		public void ShouldNotThrowArgumentException_WhenFirstAndLastNameAreValid()
		{
			var author = new Author { FirstName = "William", LastName = "Shakespeare" };
			Assert.DoesNotThrow(() => AuthorValidator.Validate(author));
		}

		[Test] // if(author.FirstName.Length<3 && author.FirstName.Length>10)
			   // ;
			   // throw new ArgumentException("")
		public void ShouldThrowArgumentException_ForFirstNameWithMoreThanTenLetters()
		{
			var author = new Author { FirstName = "Christopher", LastName = "Shakespeare" };
			Assert.That(Assert.Throws<ArgumentException>(() => AuthorValidator.Validate(author)).Message, Is.EqualTo("Author name should be between 3 and 10 characters"));
		}

		[Test] // if(author.FirstName.Length<=3 || author.FirstName.Length>10)
		public void ShouldNotThrowArgumentException_ForFirstNameWithThreeLetters()
		{
			var author = new Author { FirstName = "Ana", LastName = "Shakespeare" };
			Assert.DoesNotThrow(() => AuthorValidator.Validate(author));
		}

		[Test] // if(author.FirstName.Length<3 || author.FirstName.Length>=10)
		public void ShouldNotThrowArgumentException_ForFirstNameWithTenLetters()
		{
			var author = new Author { FirstName = "Christiana", LastName = "Shakespeare" };
			Assert.DoesNotThrow(() => AuthorValidator.Validate(author));
		}

		[Test] // if (author.LastName.Length< 3 && author.LastName.Length> 20)
			   // ;
			   // throw new ArgumentException("")
		public void ShouldThrowArgumentException_ForLastNameWithLessThanThreeLetters()
		{
			var author = new Author { FirstName = "William", LastName = "Sh" };
			Assert.That(Assert.Throws<ArgumentException>(() => AuthorValidator.Validate(author)).Message, Is.EqualTo("Author last name should be between 3 and 20 characters"));
		}

		[Test] // if (author.LastName.Length <= 3 || author.LastName.Length > 20)
		public void ShouldNotThrowArgumentException_ForLastNameWithThreeLetters()
		{
			var author = new Author { FirstName = "William", LastName = "Sha" };
			Assert.DoesNotThrow(() => AuthorValidator.Validate(author));
		}

		[Test] // if (author.LastName.Length< 3 || author.LastName.Length >= 20)
		public void ShouldNotThrowArgumentException_ForLastNameWithTwentyLetters()
		{
			var author = new Author { FirstName = "William", LastName = "van Marschalkerweerd" };
			Assert.DoesNotThrow(() => AuthorValidator.Validate(author));
		}
	}
}
