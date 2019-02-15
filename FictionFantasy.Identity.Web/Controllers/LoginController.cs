using Microsoft.AspNetCore.Mvc;

namespace FictionFantasy.Identity.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            
        }
    }
}