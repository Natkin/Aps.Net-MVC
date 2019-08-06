using Dofige.DataLibrary;
using Dofige.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dofige.Controllers
{
    public class HomeController : Controller
    {
        private IFinance Ifinance;
        public HomeController(IFinance ifinance)
        {
            Ifinance = ifinance;
        }
       [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var CategoryList = Ifinance.GetFinanceTypeList();
            ViewBag.FinTypeList = new SelectList(Ifinance.GetFinanceTypeList(), "CatID", "CatName");
            ViewBag.Categories = Ifinance.GetFinanceTypeList();
            ViewBag.Subcategory = Ifinance.GetSubCat();
            ViewBag.Subcategories = new SelectList(Ifinance.GetSubCat(), "SubCatID", "SubCatName");
            ViewBag.Types = Ifinance.GetAllTypes();
            FilterModel f = new FilterModel();
            var m = Ifinance.GetListdata(f);
            return View(m);
        }
        [HttpGet]
       public ActionResult ListPart(string filter = "")
       {
           FilterModel f = new FilterModel();
           if (string.IsNullOrEmpty(filter))
           {
               f = new FilterModel();
           }
           else
           {
               JavaScriptSerializer js = new JavaScriptSerializer();
               f = (FilterModel)js.Deserialize(filter, typeof(FilterModel));
           }
           var model = Ifinance.GetListdata(f);
           return PartialView(model);
       }
       [HttpGet]
       public ActionResult Create()
       {
           var m = new PagerModel();
           var CategoryList = Ifinance.GetFinanceTypeList();
           ViewBag.FinTypeList = new SelectList(Ifinance.GetFinanceTypeList(), "CatID", "CatName");
           ViewBag.Categories = Ifinance.GetFinanceTypeList();
           return View(m);
       }
        [HttpPost]
        public ActionResult Create(PagerModel model)
        {
            ViewBag.FinTypeList = new SelectList(Ifinance.GetFinanceTypeList(), "CatID", "CatName");
            ViewBag.Categories = Ifinance.GetFinanceTypeList();
            return View();
        }
        [HttpGet]
        public ActionResult GetSubcategory(int id=0)
        {
            ViewBag.Subcategory = Ifinance.GetSubCat();
            ViewBag.Subcategories = new SelectList(Ifinance.GetSubCat(), "SubCatID", "SubCatName");
            return PartialView();
        }
        [HttpGet]
        public ActionResult Newrecord()
        {
            var m = new PagerModel();
            var list = Ifinance.GetFinanceTypeList();
            var types = Ifinance.GetAllTypes();
            ViewBag.Records = list;
            ViewBag.Types = types;
            return View(m);
        }
        [HttpPost]
        public ActionResult Newrecord(PagerModel m)
        {
            var list = Ifinance.GetFinanceTypeList();
            var types = Ifinance.GetAllTypes();
            ViewBag.Records = list;
            ViewBag.Types = types;
            if (ModelState.IsValid)
            {
                Ifinance.Create(m);
            }
            else
            {
                return View(m);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public string NewCategory(int id, string name)
        {
            Ifinance.NewCategory(name, id);
            return "Success";
        }
        [HttpGet]
        public string NewSubCategory(int id, string name)
        {
            Ifinance.NewSubCategory(name, id);
            return "Success";
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}