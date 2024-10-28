using System;
using System.Collections.Generic;

namespace CRUD.PL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public UserViewModel() {
            Id = Guid.NewGuid().ToString();
        }
    }
}
