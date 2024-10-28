using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; } =new HashSet<Employee>();
    }
}
