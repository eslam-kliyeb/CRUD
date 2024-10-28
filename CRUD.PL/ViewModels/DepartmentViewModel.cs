using CRUD.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CRUD.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
