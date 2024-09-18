using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Interface;
using BootCamp2024.Service.Interface;

namespace BootCamp2024.Service.Implementation
{
	public class AuthorsService : IAuthorsService
	{
		private readonly IAuthorsRepository _authorsRepository;
		public AuthorsService(IAuthorsRepository authorsRepository)
		{
			_authorsRepository = authorsRepository;
		}

		public void Create(Author author)
		{
			_authorsRepository.Create(author);
		}

		public bool Delete(int id)
		{
			return _authorsRepository.Delete(id);
		}

		public IEnumerable<Author> GetAll()
		{
			return _authorsRepository.GetAll();
		}

		public Author GetById(int id)
		{
			return _authorsRepository.GetById(id);
		}

		public bool Update(Author author, int id)
		{
			return _authorsRepository.Update(author, id);
		}
	}
}
