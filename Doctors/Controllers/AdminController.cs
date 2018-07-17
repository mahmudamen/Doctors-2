using Doctors.Models;
using Doctors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc.Async;
using System.Web.Mvc;


namespace Doctors.Controllers
{
     [AuthorizeRoles("Admin" , "Doctor")]
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
        public JsonResult PatQue(int id ) {
            var query = db.Patients.Where(x => x.ID == id).SingleOrDefault();
            var patin = db.Patients.Where(x => x.PatienState == 3).SingleOrDefault();
            patin.PatienState = 4;
            query.PatienState = 3;
            db.SaveChanges();
            return Json(new { Success = true, Message = "تم نقل بيانات المريض الي شاشة الطبيب" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendtoDoct() {
            var query = db.Patients.Where(x => x.PatienState == 3).Select( x => new { x.Sorted });
            var patin = db.Patients.Where(x => x.PatienState == 3).SingleOrDefault();
            patin.PatienState = 4;

            return Json(new { Success = true, Message = "تم نقل بيانات المريض الي شاشة الطبيب" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Refund(int id) {
            var query = db.Patients.Where(x => x.ID == id).SingleOrDefault();
            query.PaidAmount = 0;
            query.IsActive = false;
            query.PatienState = 5;
            db.SaveChanges();
            return Json(new { Success = true, Message = " تم إلغاء الحجز " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RefundRp()
        {
            var query = db.vRefundRps.Select(x => new { x.ID , x.PatientName , x.ServName , x.CreateDate });

            return Json(new { aaData = query } , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            HttpFileCollectionBase ha = Request.Files;
            HttpPostedFileBase g = ha[0];
            string ename = g.FileName;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string category = HttpContext.Request.Form["cby"];
                    string vid = HttpContext.Request.Form["vid"];
                    string Galry = HttpContext.Request.Form["Galery"];
                    string Subject = HttpContext.Request.Form["Subject"];
                    string ReNamePic = HttpContext.Request.Form["ReNamePic"];



                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  
                        HttpPostedFileBase file = files[i];
                        string fname;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it. 
                        var m = Convert.ToString(DateTime.Now);
                        fname = Path.Combine(Server.MapPath("~/Img"), fname);
                        ArchPro h = new ArchPro();
                        //h.ProListFK = Convert.ToInt32(vid);
                        //h.GFK = Convert.ToInt32(Galry);
                        h.PicPath = "/Rays/Img/" + file.FileName;
                        //h.Photo = file.FileName;
                        //h.Subject = Subject;
                        h.ReNamePic = ReNamePic;
                        h.CreateBy = Convert.ToInt32(category);
                        h.CreateDate = DateTime.Now;
                        //db.ArchPro.Add(h);
                        db.SaveChanges();
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { Success = true, resulte = ename, Message = "   بنجاح" + ename + " تم إضافة الصورة" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}