using System;
using System.Threading.Tasks;

namespace CRUD.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;}
        Task<int> CompleteAsync();
    }
}
