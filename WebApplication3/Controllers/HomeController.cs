using Microsoft.AspNetCore.Mvc;

namespace CubeService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Charts()
        {
            return View();
        }
    }
}
