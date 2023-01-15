using Microsoft.AspNetCore.Mvc;

namespace WebTest.Controllers
{
    public class Productos : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
