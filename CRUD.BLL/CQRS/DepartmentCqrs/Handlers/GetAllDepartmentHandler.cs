using CRUD.BLL.CQRS.DepartmentCqrs.Queries;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.BLL.CQRS.DepartmentCqrs.Handlers
{
    public class GetAllDepartmentHandler : IRequestHandler<GetAllDepartmentQuery, IEnumerable<Department>>
    {
        private readonly DataContext _dataContext;
        public GetAllDepartmentHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
           return await _dataContext.Departments.ToListAsync();
        }
    }
}
