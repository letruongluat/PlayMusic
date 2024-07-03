using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMusic.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Manage
        public ActionResult Manage()
        {
            return View();
        }
    }
}