using CRUD.BLL.CQRS.DepartmentCqrs.Commands;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.BLL.CQRS.DepartmentCqrs.Handlers
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Department>
    {
        private readonly DataContext _dataContext;
        public UpdateDepartmentHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Department> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Departments.Update(request.department);
            await _dataContext.SaveChangesAsync();
            return await Task.FromResult(request.department);
        }
    }
}
