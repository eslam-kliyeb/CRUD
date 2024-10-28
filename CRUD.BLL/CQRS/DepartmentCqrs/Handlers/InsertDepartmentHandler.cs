using CRUD.BLL.CQRS.DepartmentCqrs.Commands;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.BLL.CQRS.DepartmentCqrs.Handlers
{
    public class InsertDepartmentHandler : IRequestHandler<InsertDepartmentCommand, Department>
    {
        private readonly DataContext _dataContext;
        public InsertDepartmentHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Department> Handle(InsertDepartmentCommand request, CancellationToken cancellationToken)
        {
           await _dataContext.Departments.AddAsync(request.department);
           await _dataContext.SaveChangesAsync();
           return await Task.FromResult(request.department);
        }
    }
}
