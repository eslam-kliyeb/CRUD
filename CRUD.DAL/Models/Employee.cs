using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace CRUD.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address {  get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActve { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public string ImageName {  get; set; }
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
