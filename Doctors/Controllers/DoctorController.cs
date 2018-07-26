using Doctors.Models;
using Doctors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doctors.Controllers
{
    [AuthorizeRoles("Admin", "Doctor", "Screen")]
    public class DoctorController : Controller
    {
        // GET: Doctor
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
        public  ActionResult ScreenS()
        {
            return View();
        }
        public JsonResult gdate()
        {
            var query = DateTime.Now;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PatTbl()
        {
            var query = db.Patients.Where(x => x.IsActive == true && x.PatienState == 3)
            .Select(p => new { p.ID, p.PatientName, p.ServName ,p.Serv, p.CreateDate , p.MedicalHistory , p.PrevDiagnosis ,p.Treatment, p.Diagnosis ,p.Examination , p.Code });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        // Get wait table 
        public JsonResult WaitTbl()
        {
            var query = db.Vwaitlists.Where(x => x.shfactive == true)
            .Select(p => new { p.ID, p.PatientName, p.ServName, p.CreateDate, p.Sorted, p.RemainingAmount });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DrugsTbl() {
            var query = db.ItemMedics.Where(x => x.IsActive == true).Select(n => new { n.ID , n.EnName , n.Materialact  });
            return Json(new {aaData = query }, JsonRequestBehavior.AllowGet);
        }
        // auto complete service name 
        public JsonResult autoserv(string Prefix)
        {
            var CityName = db.ServLists.Where(o => o.ServName.Contains(Prefix) && o.IsActive == true)
                    .Select(x => new { x.ID, x.ServName, x.PaidAmount }).Take(20).ToList();
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        // autocomplete item name modal 
        public JsonResult autoitemname(string Prefix)
        {
            var Query = db.ItemMedics.Where(o => o.EnName.Contains(Prefix)).Select(x => new { x.ID , x.EnName }).Take(20).ToList();
            return Json(Query, JsonRequestBehavior.AllowGet);
        }
        // get patient's rays
        public JsonResult PatRay(int? id) {
            var query = db.ArchProes.Where(x => x.PatientID == 29).Single().ReNamePic;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        // next patient button 
        public JsonResult NextPat(int id ) {
            var query = db.Patients.Where(x => x.ID == id).SingleOrDefault();
            query.PatienState = 4;
            db.SaveChanges();
            return Json(new { Success = true, Message = "تم انهاء زيارة المريض " }, JsonRequestBehavior.AllowGet);
        }
        // add new patient
        public JsonResult adpat(string patname, string mobile, string phone, int servID, string servname, decimal paidamount, DateTime? visitdate, bool gender, string adress, int cby, string cbyn)
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
            db.Patients.Add(m);
            db.SaveChanges();
            return Json(new { Success = true, Message = "تمت الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        // add new shift work 
        public JsonResult adshift(DateTime shiftdate, string cbyn)
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
            var query = db.ShiftLists.Count() + 1;
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
        public JsonResult MDetails(int? id) {
            if (id == null) {
                var query = db.PatientItemMedics.Select(x => new { x.ID, x.PatientID, x.ItemMedicID, x.EnName, x.Dose, x.DoseName }).Take(0);
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var query = db.PatientItemMedics.Where(x => x.PatientID == id).Select(x => new { x.ID, x.PatientID, x.ItemMedicID, x.EnName, x.Dose, x.DoseName });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            
            
        }
        public JsonResult DoseList()
        {
            var query = db.DoseLists.Select(x => new { x.ID, x.DoseName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AdDose(string vname)
        {
            var query = db.DoseLists.Where(x => x.DoseName.Equals(vname)).SingleOrDefault();
            

             if(vname != null )
            {
                DoseList dl = new DoseList();
                dl.DoseName = vname;
                dl.IsActive = true;
                db.DoseLists.Add(dl);
                db.SaveChanges();
                return Json(new { Success = true, Message = "تمت الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = true, Message = "خطاء" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}