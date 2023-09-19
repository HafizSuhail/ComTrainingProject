using Microsoft.AspNetCore.Mvc;

namespace NHibernate_CURD._20.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View("Homepage");
        }
    }
}
