using CRUD.DAL.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.CQRS.DepartmentCqrs.Commands
{
    public record RemoveDepartmentCommand(Department department) : IRequest<Department>;
}
