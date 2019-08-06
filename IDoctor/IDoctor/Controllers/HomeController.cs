using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace IDoctor.Controllers
{
    public class HomeController : Controller
    {
        LabdesktopDBEntities db = new LabdesktopDBEntities();
        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult Registry()
        {
            //List<SelectListItem> items = new List<SelectListItem>();
            //List<string> li = new List<string>();
            //var item = from i in db.Doctors
            //           select i.Surname;
            //foreach (var i in item)
            //    li.Add(i);
            //var item2 = from i in db.Doctors
            //            select i;
            //foreach (var i in item2)
            //    items.Add(new SelectListItem { Text = i.Surname, Value = i.Id.ToString() });
            //ViewBag.Lastnames = items;

            ViewData["data"] = DateTime.Now.ToString("dd.MM.yyy");

            return View();
        }
        [HttpPost]
        public ActionResult GetIndex()
        {
            ElQueue p = new ElQueue();
            p.V_Surame = Request.Params["txt"];
            p.V_Name = Request.Params["txt2"];
            p.V_Midname = Request.Params["txt3"];
            p.V_Date = DateTime.Now.ToString("dd.MM.yyy");
            p.V_hour = Request.Params["txt4"];
            p.V_minute = Request.Params["txt5"];
            db.ElQueue.Add(p);
            db.SaveChanges();

            string ln = Request.Params["txt"];
            string fn = Request.Params["txt2"];
            string mn = Request.Params["txt3"];
            string dat = DateTime.Now.ToString("dd.MM.yyy");
            string h = Request.Params["txt4"];
            string m = Request.Params["txt5"];

            ViewBag.sur = ln;
            ViewBag.nam = fn;
            ViewBag.mid = mn;
            ViewBag.dat = dat;
            ViewBag.hour = h;
            ViewBag.min = m;
            //Response.Redirect(@"~/Reports/Ticket.aspx?id=" + ln+"&nam=" + fn+"&mid="+mn+"&h="+h+"&m="+m);
            return View();
        }
        [Authorize(Roles = "doctor")]
        public ActionResult Doctor()
        {

            string queue = "";
            string hour = "";
            string minute = "";
            List<SelectListItem> par = new List<SelectListItem>();
            string date=DateTime.Now.ToString("dd.MM.yyy");
            ViewBag.Date = date;
            var param = from q in db.ElQueue
                        where q.V_Date == date
                        select q;
            foreach (var i in param)
            {
                par.Add(new SelectListItem { Text = i.V_Surame + " " + i.V_hour+":"+i.V_minute, Value = i.Id.ToString() });
                queue += i.V_Surame + " " + i.V_hour+":"+i.V_minute+"\n";
                hour += i.V_hour + "\n";
                minute += i.V_minute + "\n";
            }
            ViewBag.Time = queue;
            ViewBag.Hour = hour;
            ViewBag.Minute = minute;
            ViewBag.Times = par;
            List<SelectListItem> items = new List<SelectListItem>();
            List<string> li = new List<string>();
            var item = from i in db.Patient
                       select i;
            foreach (var i in item)
                items.Add(new SelectListItem { Text = i.Family_Name, Value = i.Pk.ToString() });
            ViewBag.Lastnames = items;

            if (Request.RequestType == "GET")
            {
                ViewBag.Text = "";
                return View();
            }
            else
            {
                string id = Request.Params["Lastnames"];
                int getid = Convert.ToInt32(id);
                var items2 = (from i in db.Patient
                              where i.Pk == getid
                              select i).FirstOrDefault<Patient>();
                string text = "";
                string text2 = "";
                string text3 = "";
                string text4 = "";
                string text5 = "";
                string text6 = "";
                string text7 = "";
                string text8 = "";
                string text9 = "";
                text += items2.Family_Name+" "+items2.Name+" "+items2.MiddleName;
                //text2 = items2.Photo;
                text3 += items2.Country+", "+items2.City+", "+items2.Address;
                text4 += items2.Age;
                text5 += items2.Birthday;
                text6 += items2.Email;
                text7 += items2.Phone_number;
                text8 += items2.LastVisitDate;
                text9 =  items2.Pk.ToString();

                ViewBag.Text = text;
                ViewBag.Photo = text2;
                ViewBag.addr = text3;
                ViewBag.tel = text7;
                ViewBag.mail = text6;
                ViewBag.visited = text8;
                ViewBag.bd = text5;
                ViewBag.age = text4;
                ViewBag.pk = text9;
                return View();
            }
        }
        public ActionResult Subcard()
        {
            return View();
        }
    }
}
