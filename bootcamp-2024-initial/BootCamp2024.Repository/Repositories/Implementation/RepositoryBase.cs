using BootCamp2024.Domain.Models;
using BootCamp2024.Repository.Repositories.Interface;

namespace BootCamp2024.Repository.Repositories.Implementation
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected static readonly List<T> Data = new List<T>();

        public void Create(T entity)
        {
            if (Data.Count == 0)
            {
                entity.Id = 1;
            }
            else
            {
                entity.Id = Data.Max(x => x.Id) + 1;
            }
            Data.Add(entity);
        }

        public bool Delete(int id)
        {
            return Data.Remove(Data.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<T> GetAll()
        {
            return Data;
        }

        public T GetById(int id)
        {
            return Data.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(T entity, int id)
        {
            int index = Data.FindIndex(x => x.Id == id);
            Data[index] = entity;
            return true;
        }
    }
}
