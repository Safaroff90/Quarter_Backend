using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quarter.Areas.Admin.ViewModels;
using Quarter.Models;

namespace Quarter.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("Member");
        //    IdentityRole role2 = new IdentityRole("SuperAdmin");
        //    IdentityRole role3 = new IdentityRole("Admin");
            


        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
          


        //    return Ok();
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new AppUser
        //    {
        //        UserName = "Safaroff",
        //        Fullname = "Saxavat Safarov",
        //    };

        //    await _userManager.CreateAsync(admin, "Sexavet123");

        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM, string returnUrl)
        {
            AppUser user = await _userManager.FindByNameAsync(loginVM.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("SuperAdmin") && !roles.Contains("Admin") && !roles.Contains("Editor"))
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersist, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You are locked out, please wait 5 minutes before trying again!");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Passwrod is incorrect!");
                return View();
            }

            if (returnUrl != null)
                return Redirect(returnUrl);

            return RedirectToAction("index", "dashboard");
        }
    }
}
