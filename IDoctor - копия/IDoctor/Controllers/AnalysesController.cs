using IDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDoctor.Controllers
{
    public class AnalysesController : Controller
    {
        //
        // GET: /Analyses/
        LabdesktopDBEntities db=new LabdesktopDBEntities();
        public ActionResult PatientData(string id)
        {
            Analise a = new Analise(id);
            return View(a);
        }
    }
}
