using CRUD.BLL.Interfaces;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dataContext;
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task AddAsync(T item)
        {
           await _dataContext.AddAsync(item);
        }
        public void Delete(T item)
        {
             _dataContext.Remove(item);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
              return (IEnumerable<T>) await _dataContext.Employees.Include(E=>E.Department).ToListAsync();
            }
            return (IEnumerable<T>) await _dataContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int Id) => await _dataContext.Set<T>().FindAsync(Id);
        public void Update(T item)
        {
            _dataContext.Update(item);
        }
    }
}
