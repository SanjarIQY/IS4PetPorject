using Microsoft.AspNetCore.Mvc;

namespace Students.MVC.Controllers
{
    public class Auth : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
