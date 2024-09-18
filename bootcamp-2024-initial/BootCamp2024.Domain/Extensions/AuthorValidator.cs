using BootCamp2024.Domain.Models;

namespace BootCamp2024.Domain.Extensions
{
    public static class AuthorValidator
    {
        public static void Validate(this Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            if(string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
            {
                throw new ArgumentException("Author must have a first and last name");
            }
            if(author.FirstName.Length<3 || author.FirstName.Length>10)
            {
                throw new ArgumentException("Author name should be between 3 and 10 characters");
            }
            if (author.LastName.Length < 3 || author.LastName.Length > 20)
            {
                throw new ArgumentException("Author last name should be between 3 and 20 characters");
            }
        }
    }
}
