using Microsoft.AspNetCore.Mvc;

namespace NHibernate_CURD._20.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult PatientsList()
        {
            return View();
        }
    }
}
