using CRUD.DAL.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.CQRS.DepartmentCqrs.Queries
{
    public record GetDepartmentQuery(int Id) : IRequest<Department>;
}
