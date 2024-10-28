using CRUD.BLL.Interfaces;
using CRUD.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IEmployeeRepository> employeeRepository;
        private DataContext _dataContext;
        public UnitOfWork(DataContext dataContext)
        {
            employeeRepository = new Lazy<IEmployeeRepository>(new EmployeeRepository(dataContext));
            _dataContext = dataContext;
        }
        public IEmployeeRepository EmployeeRepository => employeeRepository.Value;
        public async Task<int> CompleteAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dataContext.DisposeAsync();
        }
    }
}
