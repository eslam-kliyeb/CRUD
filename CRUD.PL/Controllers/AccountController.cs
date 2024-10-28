using CRUD.DAL.Models.Identity;
using CRUD.PL.Helpers;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hangfire;

namespace CRUD.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ImailSettings _mailSettings;
        public AccountController(UserManager<User> userManager, 
                                 SignInManager<User> signInManager,
                                 ImailSettings mailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailSettings = mailSettings;

        }
        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new User()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    Fname = model.FName,
                    Lname = model.LName,
                    IsAgree = model.IsAgree,
                };
                var Result  =await _userManager.CreateAsync(User,model.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                }
            }
            return View(model);
        }
        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if(User != null)
                {
                    var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
                    if(Flag == true)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(User, model.Password, false, false);
                        if (Result.Succeeded) return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError(string.Empty, "Incorrect Password");
                }
                else ModelState.AddModelError(string.Empty, "Email is not exist");
            }
            return View(model);
        }
        //Sign Out
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login)); 
        }
        //ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if(User != null)
                {
                    // Generate Token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                    //Create URL Which Send In Body of The Email
                    var url = Url.Action("ResetPassword", "Account", new { email=model.Email,token=token },Request.Scheme);
                    //Create Email
                    var Email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = url
                    };
                    //Send Email
                    BackgroundJob.Enqueue(() =>_mailSettings.SendMail(Email));
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exists");
                }
            }
            return View(nameof(ForgetPassword),model);
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        //Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user =await  _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if(result.Succeeded) return RedirectToAction(nameof(Login));
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
