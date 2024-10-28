using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.DAL.Models.Identity
{
    public class User : IdentityUser
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public bool IsAgree { get; set; }
    }
}
