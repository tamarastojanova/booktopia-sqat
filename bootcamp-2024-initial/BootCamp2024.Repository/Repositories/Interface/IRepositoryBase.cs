using BootCamp2024.Domain.Models;

namespace BootCamp2024.Repository.Repositories.Interface
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        void Create(T entity);
        bool Delete(int id);
        T GetById(int id);
        bool Update(T entity, int id);
    }
}
