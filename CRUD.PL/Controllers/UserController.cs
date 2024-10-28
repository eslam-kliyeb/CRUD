using AutoMapper;
using CRUD.DAL.Models;
using CRUD.DAL.Models.Identity;
using CRUD.PL.Helpers;
using CRUD.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, 
                              RoleManager<IdentityRole> roleManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchValue)
        {
            if (searchValue == null)
            {
                var users =await _userManager.Users.Select(U => new UserViewModel
                {
                    Id = U.Id,
                    FName = U.Fname,
                    LName = U.Lname,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync();
                return View(users);
            }
            else
            {
               var user = await _userManager.FindByEmailAsync(searchValue);
                if (user != null) {
                    var mappedUser = new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.Fname,
                        LName = user.Lname,
                        Email = user.Email,
                        Roles = _userManager.GetRolesAsync(user).Result
                    };
                    return View(new List<UserViewModel> { mappedUser });
                }
                return View(Enumerable.Empty<UserViewModel>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            if (ViewName == "Edit") TempData["Edit"] = "Edit";
            var UserVM = _mapper.Map<UserViewModel>(user);
            return View(ViewName, UserVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model,[FromRoute]string id)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var User = await _userManager.FindByIdAsync(id);
                    User.Fname = model.FName;
                    User.Lname = model.LName;
                    await _userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }
        [HttpGet]
        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id) return BadRequest();
            try
            {
                var User = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

        }
    }
}
