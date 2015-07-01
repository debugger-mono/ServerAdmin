using System.Web.Mvc;

namespace ServerAdmin.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
    }
}