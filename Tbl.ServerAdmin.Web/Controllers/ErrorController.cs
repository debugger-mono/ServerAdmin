using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tbl.ServerAdmin.Web.Controllers
{
    public class ErrorController : Controller
    {
		public ActionResult NotFound()
		{
			return View();
		}
    }
}
