namespace IdentityServer.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using IdentityServer.ViewModels;

    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("Username", "User not found");
                return View(model);
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("User", "Smth went wrong");


            return View();
        }
    }
}
