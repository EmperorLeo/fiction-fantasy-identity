using System.Threading.Tasks;
using FictionFantasy.Identity.Web.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FictionFantasy.Identity.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly TestUserStore _userStore;

        public LoginController(TestUserStore userStore)
        {
            _userStore = userStore;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            var validCredentials = _userStore.ValidateCredentials(vm.Username, vm.Password);
            if (!validCredentials)
            {
                ModelState.AddModelError("InvalidCredentials", "Invalid Credentials");
                return View(vm);
            }

            var user = _userStore.FindByUsername(vm.Username);

            await HttpContext.SignInAsync(user.SubjectId, vm.Username);

            return Redirect(vm.ReturnUrl);
        }
    }
}