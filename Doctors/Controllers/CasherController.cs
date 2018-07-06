using Syrian.Models;
using Syrian.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syrian.Controllers
{
    public class CasherController : Controller
    {
        SyDB db = new SyDB();
        public ActionResult Index()
        {
            ViewBag.itmtype = new SelectList(db.TypeList, "TypID", "TypName");
            return View();
        }
        public JsonResult removerow(int? id)
        {
            orditm orditm = db.orditm.Find(id);
            db.orditm.Remove(orditm);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult lastshift()
        {

            var query = db.Shft
            .Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult gdate()
        {
            var query = DateTime.Now;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adshf(bool a)
        {
            Shft bi = new Shft();
            bi.ShID = db.Shft.ToList().LastOrDefault().ShID + 1;
            bi.ShftDate = DateTime.Now;
            bi.ShftT = a;
            db.Shft.Add(bi);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Tlist()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 1 && x.IsActive == true)
            .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult syrianmfunk()
        {
            SyDB db = new SyDB();
            var query = db.OrdList.Where(x => x.SyianM == true)
            .Select(p => new {p.OrdID, p.GuestName, p.Address, p.TelNum , p.Mob });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult edorder(int? id)
        {
            var ucus = db.OrdList.SingleOrDefault(v => v.OrdID == id);
            
            if (ucus.IsActive)
            {
                ucus.IsActive = false;
                db.SaveChanges();
            }
            else
            {
                ucus.IsActive = true;
                db.SaveChanges();
            };
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult agnt(int? id)
        {
            var ucus = db.OrdList.SingleOrDefault(v => v.OrdID == id);
                ucus.SyianM = false;
                db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnewitm(string itmname , decimal price,int itype)
        {
            ItmList il = new ItmList();
            il.ItemID = db.ItmList.ToList().LastOrDefault().ItemID + 1;
            il.ItmName = itmname;
            il.ItmPrice = price;
            il.ItmType = itype;
            il.IsActive = true;
            db.ItmList.Add(il);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addqty(int? id)
        {
            var ucus = db.orditm.SingleOrDefault(v => v.ser == id);

           
                ucus.qty = ucus.qty + 1 ;
            ucus.totval = ucus.qty * ucus.price;
                db.SaveChanges();
            
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult sandwitch()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 3 && x.IsActive == true )
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult chiken()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 5 && x.IsActive == true)
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult falal()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 6 && x.IsActive == true)
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query } , JsonRequestBehavior.AllowGet);
        }
        public JsonResult othz()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 7 && x.IsActive == true)
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult mile()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 4 && x.IsActive == true)
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Konafa()
        {
            SyDB db = new SyDB();
            var query = db.ItmList.Where(x => x.ItmType == 2 && x.IsActive == true)
                .Select(p => new { p.ItemID, p.ItmName, p.ItmPrice });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbtype()
        {
            SyDB db = new SyDB();
            var query = db.TypeList
                .Select(p => new { p.TypID, p.TypName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ordprint(int? id)
        {
            SyDB db = new SyDB();
            if (id == null)
            {
                // id = db.OrdList.LastOrDefault().OrdID ;
                var h = db.OrdList.Max(b => (int?)b.OrdID);

                var query = db.orditm.Where(x => x.ordfk == h)
                .Select(p => new { p.ser, p.ItmList.ItmName, p.price, p.qty, p.totval });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            {
                var query = db.orditm.Where(x => x.ordfk == id)
                    .Select(p => new { p.ser, p.ItmList.ItmName, p.price, p.qty, p.totval });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ordp(int? id)
        {
            SyDB db = new SyDB();
            if (id == null)
            {
                // id = db.OrdList.LastOrDefault().OrdID ;
                var h = db.OrdList.Max(b => (int?)b.OrdID);

                var query = db.orditm.Where(x => x.ordfk == h)
                .Select(p => new { p.ser, p.ItmList.ItmName, p.price, p.qty, p.totval });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            {
                var query = db.orditm.Where(x => x.ordfk == id)
                    .Select(p => new { p.ser, p.ItmList.ItmName, p.price, p.qty, p.totval });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ORDlst()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrdID;
            var query = db.OrdList.Where(x => x.OrdID == m )
                .Select(s => s.GuestName );
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ORDaddress()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrdID;
            var query = db.OrdList.Where(x => x.OrdID == m)
                .Select(s => s.Address);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ORDtel()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrdID;
            var query = db.OrdList.Where(x => x.OrdID == m)
                .Select(s => s.TelNum);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ORDmob()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrdID;
            var query = db.OrdList.Where(x => x.OrdID == m)
                .Select(s => s.Mob);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult topten()
        {
            var m =  from r in db.OrdList
                     orderby r.GuestName descending
                     group r by r.GuestName into grp
                     select  new { key = grp.Key, cnt = grp.Count() }  ;
            
            return Json( new { aaData = m } , JsonRequestBehavior.AllowGet);
        }
        public JsonResult toptenord()
        {
            var m = from r in db.orditm 
                    orderby r.ItmList.ItmName descending
                    group r by r.ItmList.ItmName into grp
                    select new { key = grp.Key, cnt = grp.Count() };

           
            return Json(new { aaData = m }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ORDlstnum()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrderFk;
            //var query = db.OrdList.Where(x => x.OrdID == m )
            //    .Select(s => s.GuestName);
            return Json(m, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Ordpk()
        {
            SyDB db = new SyDB();
            var m = db.OrdList.ToList().LastOrDefault().OrdID;
            //var query = db.OrdList.Where(x => x.OrdID == m )
            //    .Select(s => s.GuestName);
            return Json(m, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Totfuc(int? id)
        {
            SyDB db = new SyDB();
            var mx = db.Shft
             .Count();
            var query = db.vOrdList
             .Where(p => p.shitfk == mx && p.IsActive == true)
             .Sum(c => (decimal?)c.totval) ?? 0;

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult cashrpt(int? id)
        {
            SyDB db = new SyDB();
           
            var query = db.vcashrp.Where(z => z.shid == id).
                Select(q => new {q.itmid , q.ItmName ,q.totval });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult explst()
        {
            var query = db.vExpList.
                Select(q => new { q.ExpID, q.ExpName, q.ExpVal });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult shiftdate(int id)
        {
            var query = db.Shft.Where(s => s.ShID == id)
            .Select(p => p.ShftDate.ToString());
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult eddguest(int? id )
        {
            SyDB db = new SyDB();

            var query = db.vOrdList.Where(z => z.shitfk == id)
                .Select(q => new {q.OrdID, q.OrderFk, q.GuestName ,q.IsActive });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Totfuck(int? id)
        {
            SyDB db = new SyDB();
            var query = db.orditm
             .Where(p => p.ordfk == id)
             .Sum(c => (decimal?)c.totval) ?? 0;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adORD(string gsname, int shvt , string address , string tel , string mob , bool? syrim)
        {
            SyDB db = new SyDB();
            var ca = db.OrdList.ToList().LastOrDefault().OrdID + 1;
            var ddd = db.OrdList.Where(x => x.shitfk == shvt).Max(b => (int?)b.OrderFk) + 1 ?? 1;
            OrdList bi = new OrdList();
            bi.OrdID = ca;
            bi.shitfk = shvt;
            bi.GuestName = gsname;
            bi.OrdDate = DateTime.Now;
            bi.OrderFk = ddd;
            bi.IsActive = true;
            bi.Address = address;
            bi.TelNum = tel;
            bi.Mob = mob;
            bi.SyianM = syrim;
            db.OrdList.Add(bi);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addexp(int idexp ,DateTime exdate ,decimal expval ,string remrk)
        {
            SyDB db = new SyDB();
            var ca = db.ExpDaily.ToList().LastOrDefault().Ser + 1;       
            ExpDaily bi = new ExpDaily();
            bi.Ser = ca;
            bi.ExpID = idexp;
            bi.ExpVal = expval;
            bi.ExpDate = exdate;
            bi.Remark = remrk;
            db.ExpDaily.Add(bi);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adacount(string expnme)
        {
            SyDB db = new SyDB();
            var ca = db.ExpList.ToList().LastOrDefault().ExpID + 1;
            ExpList bi = new ExpList();
            bi.ExpID = ca;
            bi.ExpName = expnme;
            bi.IsActive = true;
            db.ExpList.Add(bi);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult aditm(int itmfk, int ordfk, decimal pr, int shi, int cby)
        {
            SyDB db = new SyDB();
            var ca = db.orditm.ToList().LastOrDefault().ser + 1;
            //     var dk = db.OrdList.Where(x => x.shitfk == shvt).s + 1;
            //var ddd = db.OrdList.Where(x => x.shitfk == shvt).Max(b => (int?)b.OrderFk) + 1 ?? 1;
            orditm bi = new orditm();
            bi.ser = ca;
            bi.ordfk = ordfk;
            bi.itemfk = itmfk;
            bi.price = pr;
            bi.qty = 1;
            bi.totval = pr * 1;
            bi.createdate = DateTime.Now;
            bi.shid = shi;
            bi.createby = cby;
            db.orditm.Add(bi);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
