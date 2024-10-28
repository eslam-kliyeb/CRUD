using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
