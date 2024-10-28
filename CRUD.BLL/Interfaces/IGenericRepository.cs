using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
    }
}
