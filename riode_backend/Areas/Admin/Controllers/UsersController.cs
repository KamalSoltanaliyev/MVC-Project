using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using riode_backend.Areas.Admin.ViewModels;
using riode_backend.Contexts;
using riode_backend.Models;
using System;

namespace riode_backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class UsersController : Controller
    {
        private readonly RiodeDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(RiodeDbContext context, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var UserRoles = new List<UsersViewModel>();

            foreach (var user in users)
            {
                if (user.UserName != User.Identity.Name)
                {
                    var rolesForUser = await _userManager.GetRolesAsync(user);
                    UserRoles.Add(new UsersViewModel
                    {
                        User = user,
                        Roles = rolesForUser,
                    });

                }

            }
            return View(UserRoles);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactive(string id)
        {
            var user = await _userManager.FindByNameAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Deactive")]
        public async Task<IActionResult> DeactiveOrActiveUser(string id)
        {
            var user = await _userManager.FindByNameAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.IsActive)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Users");

        }

        public async Task<IActionResult> UserUpdateRole(string id)
        {
            ViewBag.AllRoles = await _roleManager.Roles.ToListAsync();

            var user = await _userManager.FindByNameAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null)
            {
                return NotFound();
            }

            UsersViewModel model = new()
            {
                User = user,
                Roles = roles
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UserUpdateRole(string id, UsersRoleUpdateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(id);
            UsersViewModel users = new();
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null) { return NotFound(); }
            await _userManager.RemoveFromRolesAsync(user, roles);
            foreach (var item in model.Roles)
            {
                await _userManager.AddToRoleAsync(user, item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
