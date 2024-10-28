using CRUD.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByName(string searchValue);
    }
}
