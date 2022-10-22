using Microsoft.AspNetCore.Mvc;

namespace WepApi
{
    public class CarController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}