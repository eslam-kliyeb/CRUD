using AutoMapper;
using CRUD.DAL.Models;
using CRUD.DAL.Models.Identity;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CRUD.PL.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>().ReverseMap();
        }
    }
}
