using Anime.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anime.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerVM.Username,
                Email = registerVM.Email
            };

            var identityResult = await userManager.CreateAsync(IdentityUser,registerVM.Password);   


            if (identityResult.Succeeded)
            {
                var roleidentityResult = await userManager.AddToRoleAsync(IdentityUser,"User");

                if(roleidentityResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }


            }
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl )
        {
            var model = new LoginVM
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var signinResult = await signInManager.PasswordSignInAsync(loginVM.Username,loginVM.Password,false,false );
            if(signinResult != null && signinResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginVM.ReturnUrl))
                {
                    return Redirect(loginVM.ReturnUrl);
                }
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
