using CRUD.BLL.CQRS.DepartmentCqrs.Queries;
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
    public class GetDepartmentHandler : IRequestHandler<GetDepartmentQuery, Department>
    {
        private readonly DataContext _dataContext;
        public GetDepartmentHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Department> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.Departments.FindAsync(request.Id);
        }
    }
}
