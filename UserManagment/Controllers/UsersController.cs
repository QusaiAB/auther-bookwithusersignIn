using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagment.ViewModel;

namespace UserManagment.Controllers
{   [Authorize (Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManagment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManagment  ,RoleManager<IdentityRole> roleManager)
        {
            _userManagment = userManagment;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {  
            var Users = await _userManagment.Users.Select( user => new UserViewModel
            {
                Id =  user.Id,
                Email =  user.Email,
              //  Roles = _userManagment.GetRolesAsync(user).Result
             // Roles = _userManagment.GetRolesAsync(user).ConfigureAwait(false)
             // .GetAwaiter().GetResult()
            // Roles=  _userManagment.GetRolesAsync(user).GetAwaiter().GetResult()

            }).ToListAsync();
            
        
            return View(Users);
        }
        public async Task<IActionResult> Delete(string userid)
        {
            var user = await _userManagment.FindByIdAsync(userid);

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CDelete(string userid)
        {
            var user = await _userManagment.FindByIdAsync(userid);
            var role = await _userManagment.IsInRoleAsync(user, "Admin");
            if(role)
            {
                ViewBag.Message = " you cant delete admin";
                return RedirectToAction(nameof(Index));
            }

            await _userManagment.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> MangeRoles(string userid)
        {
            var user = await _userManagment.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var viewmodel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                //Roles = roles.Select(role => new RoleViewModel
                //{
                //    RoleId = role.Id,
                //    RoleName = role.Name,
                    
                //    IsSelected = _userManagment.IsInRoleAsync(user, role.Name).Result

                //}).ToList()

            };
            

            return View(viewmodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MangeRoles(UserRolesViewModel model)
        {
            var user = await _userManagment.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }
            var Userroles = await _userManagment.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(role.IsSelected && !Userroles.Any(r=> r== role.RoleName) )
                {
                   await _userManagment.AddToRoleAsync(user, role.RoleName);
                }
                if ( role.RoleName!="Admin")
                {
                    if (!role.IsSelected && Userroles.Any(r => r == role.RoleName))
                {
                    await _userManagment.RemoveFromRoleAsync(user,role.RoleName);
                } 
                }
              

            }
            return RedirectToAction(nameof(Index));
        }
    }
}

    

