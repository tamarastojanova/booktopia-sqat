using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Interface;

namespace BootCamp2024.Repository.Repositories.Implementation
{
    public class AuthorsRepository : RepositoryBase<Author>, IAuthorsRepository
    {
        static AuthorsRepository()
        {
            Data.AddRange(_authors);
        }

        private static readonly List<Author> _authors = new List<Author>
        {
            new Author{ Id = 1, FirstName = "William", LastName = "Shakespeare", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Shakespeare.jpg/702px-Shakespeare.jpg" },
            new Author{ Id = 2, FirstName = "Agatha", LastName = "Christie", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png" },
            new Author{ Id = 3, FirstName = "Barbara", LastName = "Cartland", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/Dame_Barbara_Cartland_Allan_Warren.jpg/330px-Dame_Barbara_Cartland_Allan_Warren.jpg"},
            new Author{ Id = 4, FirstName = "Harold", LastName = "Robbins", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Harold_Robbins_%281979%29.jpg/330px-Harold_Robbins_%281979%29.jpg"},
        };
    }
}
