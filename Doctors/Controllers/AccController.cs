using Syrian.Models;
using Syrian.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syrian.Controllers
{
    public class AccController : Controller
    {
        // GET: Acc
        DB db = new DB();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult expfunc(DateTime s , DateTime e)
        {
            var query = db.ex(s,e)
            .Select(p => new { p.expname, p.expval });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult incomfunc(int? s)
        {
            var query = db.incomshfit(s)
            .Select(p => new { p.ItmName, p.totval ,p.qty});
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult shiftdate(int id)
        {
            var query = db.Shft.Where(s => s.ShID == id)
            .Select(p => p.ShftDate.ToString());
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}