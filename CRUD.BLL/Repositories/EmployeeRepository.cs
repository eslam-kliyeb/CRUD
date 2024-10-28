using CRUD.BLL.Interfaces;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models;
using System.Collections.Generic;
using System.Linq;


namespace CRUD.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly DataContext _dataContext;
        public EmployeeRepository(DataContext dataContext) : base(dataContext) { 
            _dataContext = dataContext;
        
        }
        public IEnumerable<Employee> GetEmployeesByName(string searchValue)
        {
            return _dataContext.Employees.Where(E => E.Name.Contains(searchValue));
        }
    }
}
