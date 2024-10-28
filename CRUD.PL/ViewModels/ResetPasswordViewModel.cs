using System.ComponentModel.DataAnnotations;

namespace CRUD.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password Dosen't Match")]
        public string ConfirmNewPassword { get; set; }
    }
}
