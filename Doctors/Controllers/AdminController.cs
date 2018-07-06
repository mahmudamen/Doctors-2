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
        //public JsonResult lastshift()
        //{

        //    var query = db.Shft
        //    .Count();
        //    return Json(query, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult gdate()
        {
            var query = DateTime.Now;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult adshf(bool a)
        //{
        //    Shft bi = new Shft();
        //    bi.ShID = db.Shft.ToList().LastOrDefault().ShID + 1 ;
        //    bi.ShftDate = DateTime.Now ;
        //    bi.ShftT = a;
        //    db.Shft.Add(bi);
        //    db.SaveChanges();
        //    return Json(new { Success = true },JsonRequestBehavior.AllowGet);

        //}
        public JsonResult PatTbl()
        {
            DB db = new DB();
            var query = db.Patients.Where(x =>  x.IsActive == true)
            .Select(p => new { p.ID, p.PatientName, p.Serv ,p.PaidAmount });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WaitTbl()
        {
            DB db = new DB();
            var query = db.Patients.Where(x => x.IsActive == true)
            .Select(p => new { p.ID, p.PatientName, p.ServName ,p.CreateDate ,p.RemainingAmount});
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult autoserv(string Prefix)
        {
            var CityName = db.ServLists.Where(o => o.ServName.Contains(Prefix) && o.IsActive == true)
                    .Select(x => new { x.ID, x.ServName, x.PaidAmount }).Take(20).ToList();
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        // add new patient
        public JsonResult adpat(string patname, string mobile,string phone,int serv ,string servname,decimal paidamount,DateTime? birthdate,bool gender,string adress, int cby, string cbyn)
        {
            Patient m = new Patient();
            m.PatientName = patname;
            m.Mobile = mobile;
            m.Phone = phone;
            m.Address = adress;
            m.Serv = serv;
            m.PaidAmount = paidamount;
            m.ServName = servname;
            m.Gender = gender;
            m.CreateBy = cby;
            m.CreateDate = DateTime.Now;
            db.Patients.Add(m);
            db.SaveChanges();
            return Json(new { Success = true, Message = "تمت الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult Konafa()
        //{
        //    DB db = new DB();
        //    var query = db.ItmList.Where(x => x.ItmType == 2 && x.IsActive == true)
        //        .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
        //    return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ordprint(int? id )
        //{
        //    DB db = new DB();
        //    if (id == null)
        //    {
        //        // id = db.OrdList.LastOrDefault().OrdID ;
        //        var h = db.OrdList.Max(b => (int?)b.OrdID);

        //        var query = db.orditm.Where(x => x.ordfk == h)
        //        .Select(p => new { p.ser, p.ItmList.ItmName, p.price, p.qty , p.totval});
        //        return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        //    }
        //    { 
        //    var query = db.orditm.Where(x => x.ordfk == id)
        //        .Select(p => new { p.ser, p.ItmList.ItmName, p.price , p.qty , p.totval});
        //    return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public JsonResult ORDlst()
        //{
        //    DB db = new DB();
        //    var m = db.OrdList.ToList().LastOrDefault().GuestName;
        //    //var query = db.OrdList.Where(x => x.OrdID == m )
        //    //    .Select(s => s.GuestName);
        //    return Json(m , JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ORDlstnum()
        //{
        //    DB db = new DB();
        //    var m = db.OrdList.ToList().LastOrDefault().OrderFk;
        //    //var query = db.OrdList.Where(x => x.OrdID == m )
        //    //    .Select(s => s.GuestName);
        //    return Json(m, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Ordpk()
        //{
        //    DB db = new DB();
        //    var m = db.OrdList.ToList().LastOrDefault().OrdID;
        //    //var query = db.OrdList.Where(x => x.OrdID == m )
        //    //    .Select(s => s.GuestName);
        //    return Json(m, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult Totfuc(int? id)
        //{
        //    DB db = new DB();
        //   var query = db.orditm
        //    .Where(p => p.createdate == DateTime.Today)
        //    .Sum(c => (decimal?)c.totval) ?? 0;
        //    return Json(query,JsonRequestBehavior.AllowGet);
        //}
        // public JsonResult adORD( string gsname , int shvt    )
        // {
        //     DB db = new DB();
        //    var ca  = db.OrdList.ToList().LastOrDefault().OrdID + 1;
        ////     var dk = db.OrdList.Where(x => x.shitfk == shvt).s + 1;
        //     var ddd = db.OrdList.Where(x => x.shitfk == shvt).Max(b => (int?)b.OrderFk) + 1 ?? 1;



        //     OrdList bi = new OrdList();
        //     bi.OrdID = ca ;
        //     bi.shitfk = shvt;
        //     bi.GuestName = gsname  ;
        //     bi.OrdDate = DateTime.Now;
        //     bi.OrderFk = ddd ;
        //     bi.IsActive = true;
        //     db.OrdList.Add(bi);
        //     db.SaveChanges();
        //     return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        // }
        //public JsonResult aditm(int itmfk ,int  ordfk ,decimal pr , int shi , int cby )
        //{
        //    DB db = new DB();
        //    var ca = db.orditm.ToList().LastOrDefault().ser + 1;
        //    //     var dk = db.OrdList.Where(x => x.shitfk == shvt).s + 1;
        //    //var ddd = db.OrdList.Where(x => x.shitfk == shvt).Max(b => (int?)b.OrderFk) + 1 ?? 1;
        //    orditm bi = new orditm();
        //    bi.ser = ca;
        //    bi.ordfk = ordfk;
        //    bi.itemfk = itmfk;
        //    bi.price = pr;
        //    bi.qty = 1;
        //    bi.totval = pr * 1 ;
        //    bi.createdate = DateTime.Now;
        //    bi.shid = shi;
        //    bi.createby = cby;
        //    db.orditm.Add(bi);
        //    db.SaveChanges();
        //    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        //}
    }
}