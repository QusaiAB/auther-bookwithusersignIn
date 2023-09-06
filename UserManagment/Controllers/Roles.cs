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
{ [Authorize (Roles = "Admin")]
    public class Roles : Controller
    { private readonly  RoleManager<IdentityRole> _Rolemanager;
        public Roles(RoleManager <IdentityRole> roleManager )
        {
            _Rolemanager = roleManager;

        }
        public async Task <IActionResult> Index()
        {
            var role = await _Rolemanager.Roles.ToListAsync();
            return View(role);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFromViewModel model)
        {  
            if (!ModelState.IsValid)
            {
                return View("Index", await _Rolemanager.Roles.ToListAsync());
            }
            if (await _Rolemanager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "the Role Is Exists!");
                return View("Index", await _Rolemanager.Roles.ToListAsync()); 
            }
            await _Rolemanager.CreateAsync(new IdentityRole(model.Name.Trim()));
            return RedirectToAction(nameof(Index));


        }


    }
}
