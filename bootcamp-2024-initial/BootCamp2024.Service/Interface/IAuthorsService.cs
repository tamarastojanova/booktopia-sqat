using BootCamp2024.Domain.Models;

namespace BootCamp2024.Service.Interface
{
	public interface IAuthorsService
	{
		public void Create(Author author);
		public bool Delete(int id);
		public IEnumerable<Author> GetAll();
		public Author GetById(int id);
		public bool Update(Author author, int id);
	}
}
