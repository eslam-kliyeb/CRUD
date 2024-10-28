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
    public class GetDepartmentsByNameHandler : IRequestHandler<GetDepartmentsByNameQuery, IEnumerable<Department>>
    {
        private readonly DataContext _dataContext;
        public GetDepartmentsByNameHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<Department>> Handle(GetDepartmentsByNameQuery request, CancellationToken cancellationToken)
        {
            return  _dataContext.Departments.Where(D => D.Name.Contains(request.searchValue));
        }
    }
}
