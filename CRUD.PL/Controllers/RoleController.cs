using AutoMapper;
using CRUD.DAL.Models.Identity;
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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager,
                               UserManager<User> userManager,
                              IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchValue)
        {
            if (searchValue == null)
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRoles = _mapper.Map<IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRoles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(searchValue);
                if (role != null)
                {
                    var mappedrole = _mapper.Map<RoleViewModel>(role);
                    return View(new List<RoleViewModel> { mappedrole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = _mapper.Map<IdentityRole>(model);
                await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role == null) return NotFound();
            if (ViewName == "Edit") TempData["Edit"] = "Edit";
            var UserVM = _mapper.Map<RoleViewModel>(Role);
            return View(ViewName, UserVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(id);
                    Role.Name = model.Name;
                    await _roleManager.UpdateAsync(Role);
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
                var User = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role =await _roleManager.FindByIdAsync(roleId);
            if(role is null) return BadRequest();
            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsInRole = await _userManager.IsInRoleAsync(user, role.Name),
                };
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserRoleViewModel> users) 
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if(appUser is null) return NotFound();
                    if(user.IsInRole && !await _userManager.IsInRoleAsync(appUser, role.Name))
                    {
                        await _userManager.AddToRoleAsync(appUser, role.Name);
                    }
                    else if (!user.IsInRole && await _userManager.IsInRoleAsync(appUser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction(nameof(Edit), new {id = roleId});
            }
            return View(users);
        }
    }
}
