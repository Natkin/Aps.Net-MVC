using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDoctor.Controllers
{
    public class PatientController : Controller
    {
        //
        // GET: /Patient/
        LabdesktopDBEntities db = new LabdesktopDBEntities();
        //[HttpGet]
        public ActionResult Card(string id)
        {
            ViewData["id"] = id;
            int key = Convert.ToInt32(id);
            //if (id == null)
            //    return new HttpStatusCodeResult(404);
            //else
            //{
            string fio = "";
            string addr = "";
            string fot = "";
            var param = from par in db.Patient
                        where par.Pk == key
                        select par;
            foreach (var i in param)
            {
                fio += i.Family_Name + " " + i.Name + " " + i.MiddleName;
                addr += i.Country + ", " + i.City + ", " + i.Address;
                fot = i.Photo;
            }
            ViewBag.fio = fio;
            ViewBag.addr = addr;
            ViewBag.photo = fot;
            //}

            return View();
        }

        public ActionResult Index()
        {
            string id=Request.Params["idText"];
                int key = Convert.ToInt32(id);
            var item = from items in db.Patient
                       where items.Pk == key
                       select items.Pk;
           
            PatInfo p = new PatInfo();
            foreach (var i in item)
            {
                p.PatFk = i;
            }
            p.Diagnisis = Request.Params["diag"];
            p.Zhaloby = Request.Params["comp"];
            p.Reccomendation = Request.Params["med"];
            p.Date = DateTime.Now;
            db.PatInfo.Add(p);
            db.SaveChanges();

            //Add medicines
            Liky l = new Liky();
            l.MedName = Request.Params["stuff[]"];
            l.Dosage = Request.Params["stuff2[]"];
            l.Usage = Request.Params["stuff3[]"];
            l.Data = DateTime.Now;
            foreach (var j in item)
            {
                l.PatFk = j;
            }
            db.Liky.Add(l);
            db.SaveChanges();
            return PartialView();
        }
        public ActionResult TextAdding(string id)
        {
            int key = Convert.ToInt32(id);
            var item = from items in db.Patient
                       where items.Pk == key
                       select items;
            PatInfo p = new PatInfo();
            foreach (var i in item)
            {
                p.PatFk = i.Pk;
            }
            p.Diagnisis = Request.Params["diag"];
            p.Zhaloby = Request.Params["comp"];
            p.Reccomendation = Request.Params["med"];
            p.Date = DateTime.Now;
            db.PatInfo.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult MedAdding(string id)
        {
            int key = Convert.ToInt32(id);
            var item = from items in db.Patient
                       where items.Pk == key
                       select items;
            ////Add medicines
            Liky l = new Liky();
            l.MedName = Request.Params["stuff[]"];
            l.Dosage = Request.Params["stuff2[]"];
            l.Usage = Request.Params["stuff3[]"];
            l.Data = DateTime.Now;
            foreach (var j in item)
            {
                l.PatFk = j.Pk;
            }
            db.Liky.Add(l);
            db.SaveChanges();
            return View();
        }
    }
}

