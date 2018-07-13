using Doctors.Models;
using Doctors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doctors.Controllers
{
     [AuthorizeRoles("Admin")]
    public class AdminController : Controller
    {
        DB db = new DB();
        public ActionResult Index()
        {
        //    ViewBag.itmtype = new SelectList(db.TypeList, "TypID", "TypName");
            return View();
        }
        public ActionResult cash()
        {
      //      ViewBag.itmtype = new SelectList(db.TypeList, "TypID", "TypName");
            return View();
        }
        public JsonResult gdate()
        {
            var query = DateTime.Now;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PatTbl()
        {
            DB db = new DB();
            var query = db.VpatientLists.Where(x =>  x.IsActive == true )
            .Select(p => new { p.ID, p.PatientName, p.Serv ,p.PaidAmount ,p.ShiftID });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WaitTbl()
        {
            DB db = new DB();
            var query = db.Vwaitlists.Where(x => x.shfactive == true)
            .Select(p => new { p.ID, p.PatientName, p.ServName ,p.CreateDate ,p.Sorted,p.RemainingAmount ,p.ShiftID});
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult autoserv(string Prefix)
        {
            var CityName = db.ServLists.Where(o => o.ServName.Contains(Prefix)  && o.IsActive == true)
                    .Select(x => new { x.ID, x.ServName, x.PaidAmount }).Take(20).ToList();
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        // add new patient
        public JsonResult adpat(string patname, string mobile,string phone,int servID,int shftid, string servname,decimal paidamount,DateTime? visitdate, bool gender,string adress, int cby, string cbyn)
        {
            Patient m = new Patient();
            m.PatientName = patname;
            m.Mobile = mobile;
            m.Phone = phone;
            m.Address = adress;
            m.Serv = servID;
            m.PaidAmount = paidamount;
            m.ServName = servname;
            m.Gender = gender;
            m.CreateBy = cby;
            m.IsActive = true;
            m.PatienState = 1;
            m.CreateDate = visitdate;
            m.ShiftID = shftid;
            db.Patients.Add(m);
            db.SaveChanges();
            return Json(new { Success = true, Message = "تمت الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        // add new shift work 
        public JsonResult adshift(DateTime shiftdate , string cbyn)
        {
            ShiftList shft = new ShiftList();
            var isactive = db.ShiftLists.Where(x => x.IsActive == true).ToList();
            isactive.ForEach(x => x.IsActive = false);
            db.SaveChanges();
            
            shft.ShftDate = shiftdate;
            shft.IsActive = true;
            db.ShiftLists.Add(shft);
            db.SaveChanges();
            return Json(new { Success = true, Message = "تمت الإضافة  شفت بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        // function get last shift number 
        public JsonResult Tcount()
        {
            var query = db.ShiftLists.Count() + 1 ;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult shiftcount()
        {
            var query = db.ShiftLists.Where(x => x.IsActive == true).Single().ID;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult shiftdate()
        {
            var query = db.ShiftLists.Where(x => x.IsActive == true).Single().ShftDate;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult sendtowaitlist(int ID , int Shft)
        {
            var query = db.Patients.Where(x => x.ID == ID && x.ShiftID == Shft && x.IsActive == true).ToList();
            var sort = db.Patients.Where(x => x.ShiftID == Shft && x.IsActive == true).Select(s => s.Sorted).Max();
            query.ForEach(x => x.Sorted  = sort + 1);
            query.ForEach(x => x.PatienState = 2);
            db.SaveChanges();
            return Json(new { Success = true, Message = "تمت اضافته الي قائمة الانتظار "}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShiftReportTbl()
        {
            var query = db.VshiftTotals.Select(x => new { x.ShiftID, x.ShftDate, x.total });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
    }
}