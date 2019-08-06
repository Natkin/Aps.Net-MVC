using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using IDoctor.Models;

namespace IDoctor.Controllers
{
    public class DocMainController : Controller
    {
        private LabdesktopDBEntities db = new LabdesktopDBEntities();
        static List<ElQueue> li = new List<ElQueue>();
        //public DocMainController()
        //{
        //    var param = from p in db.ElQueue
        //                select p;
        //    foreach (var i in param)
        //    {
        //        li.Add(new ElQ { Surname = i.V_Surame, Hour = i.V_hour, Minute = i.V_minute });
        //    }
        //}
        //
        // GET: /DocMain/

        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Patient
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Family_Name.Contains(searchString));
            }
            ViewModel vm = new ViewModel();

            students = students.OrderBy(s => s.Family_Name);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            ViewBag.FN = "Name";
            ViewBag.LN = "Last Name";
            ViewBag.MN = "Middle Name";
            ViewBag.Dat = "Last visit date";
            ViewBag.Qu = "Queue";
            ViewBag.TD = DateTime.Now.ToString("dd.MM.yyy");

            string td=DateTime.Now.ToString("dd.MM.yyy");
            List<ElQueue> li = new List<ElQueue>();
            var item = from i in db.ElQueue
                       where i.V_Date==td
                       select i;
            foreach (var i in item)
                li.Add(i);
            ViewBag.Q = li.ToList();

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /DocMain/Details/5

        public ActionResult Details(int id = 0)
        {
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // GET: /DocMain/Create

        public ActionResult Create()
        {
            ViewBag.DoctorFk = new SelectList(db.Doctors, "Id", "Name");
            return View();
        }

        //
        // POST: /DocMain/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorFk = new SelectList(db.Doctors, "Id", "Name", patient.DoctorFk);
            return View(patient);
        }

        //
        // GET: /DocMain/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorFk = new SelectList(db.Doctors, "Id", "Name", patient.DoctorFk);
            return View(patient);
        }

        //
        // POST: /DocMain/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorFk = new SelectList(db.Doctors, "Id", "Name", patient.DoctorFk);
            return View(patient);
        }

        //
        // GET: /DocMain/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // POST: /DocMain/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult Deleting(int id = 0)
        {
            ElQueue patient = db.ElQueue.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        //
        // POST: /DocMain/Delete/5

        [HttpPost, ActionName("Deleting")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletingConfirmed(int id)
        {
            ElQueue patient = db.ElQueue.Find(id);
            db.ElQueue.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult TB()
        {
            return View();
        }
    }
}