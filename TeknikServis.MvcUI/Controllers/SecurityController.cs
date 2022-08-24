using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeknikServis.MvcUI.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Giris()
        {
            return View();
        }
    }
}