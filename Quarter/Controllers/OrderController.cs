using Microsoft.AspNetCore.Mvc;

namespace Quarter.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
