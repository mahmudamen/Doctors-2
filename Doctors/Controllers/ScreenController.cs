using Doctors.Models;
using Doctors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doctors.Controllers
{
    public class ScreenController : Controller
    {
        // GET: Screen
        [AuthorizeRoles("Admin", "Doctor", "Screen")]
        public ActionResult Index()
        {
            return View();
        }
    }
}