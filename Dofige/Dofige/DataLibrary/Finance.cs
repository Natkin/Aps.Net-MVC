using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dofige.DataModels;
using Dofige.DataLibrary.ModelEntities;

namespace Dofige.DataLibrary
{
    public class Finance:IFinance
    {
        public PagerModel GetListdata(FilterModel filter)
        {
            using (Entities db = new Entities())
            {
                var item = (from i in db.FinanceMain
                            join cat in db.Category on i.CatID equals cat.CatId
                            where (i.CatID == filter.catid || filter.catid == null) &&
                            (i.SubCatID == filter.subcatid || filter.subcatid == null) &&
                            (cat.TypeID == filter.typeID || filter.typeID == null) &&
                            ((i.Data >= filter.start_data || filter.start_data == null) && (i.Data < filter.end_data || filter.end_data == null))
                            select new FinanceModel
                            {
                                finID = i.FinanceMainId,
                                amount = (decimal)i.Amount,
                                data = (DateTime)i.Data,
                                Category = new CategoryModel
                                {
                                    CatID = (int)i.CatID,
                                    CatName = db.Category.Where(x => x.CatId == i.CatID).Select(x => x.CatName).FirstOrDefault(),
                                    Subcategory = new SubCategoryModel
                                    {
                                        SubCatID=(int)i.SubCatID,
                                        SubCatName = db.Subcategory.Where(x => x.SubCatId == i.SubCatID).Select(x => x.SubCatname).FirstOrDefault()
                                    },
                                    TypeModel = new ExpencesTypeModel
                                    {
                                        typeID=(int)cat.TypeID,
                                        typeName = db.Type.Where(x => x.TypeId == cat.TypeID).Select(x => x.Typename).FirstOrDefault()
                                    }
                                }
                            }).ToList();
                var page = 7;
                if (filter.current_page == null)
                {
                    filter.current_page = 1;
                }
                PagerModel p = new PagerModel()
                {
                    Filter = filter,
                    //Finance = item,
                    Items_per_page = 10,
                    Totalitems = item.Count,
                    TotalPages = (int)Math.Ceiling((decimal)item.Count / page),
                    Finance = item.Skip((Convert.ToInt32(filter.current_page) - 1) * page).Take(page).ToList()
                };
                return p;
            }           
        }


        public void Create(PagerModel model)
        {
            using (Entities db = new Entities())
            {
                FinanceMain m = new FinanceMain();
                for (int i = 0; i < model.Finance.Count; i++)
                {
                    m.Amount = Convert.ToDecimal(model.Finance[i].amount);
                    m.CatID = model.Finance[i].Category.CatID;
                    m.SubCatID = model.Finance[i].Category.Subcategory.SubCatID;
                    m.Data = model.Finance[i].data;
                    db.FinanceMain.Add(m);
                    db.SaveChanges();
                }                
            }
        }

        public List<CategoryModel> GetFinanceTypeList()
        {
            List<CategoryModel> m = new List<CategoryModel>();
            using (Entities db = new Entities())
            {
                    var item = (from items in db.Category
                                select new CategoryModel
                                {
                                    CatID=items.CatId,
                                    CatName=items.CatName,
                                    Subcategories = (from sub in db.Subcategory
                                                     where sub.CatID==items.CatId
                                                     select new SubCategoryModel
                                    {
                                        SubCatID = sub.SubCatId,
                                        SubCatName = sub.SubCatname
                                    }).ToList()
                                }).ToList();
                    return item;
            }
        }
        public List<SubCategoryModel> GetSubCat()
        {
            using (Entities db = new Entities())
            {
                var item = (from items in db.Subcategory
                            select new SubCategoryModel
                            {
                                SubCatID = items.SubCatId,
                                SubCatName = items.SubCatname
                            }).ToList();
                return item;
            }
        }
        public List<ExpencesTypeModel> GetAllTypes()
        {
            using (Entities db = new Entities())
            {
                var item = (from i in db.Type
                            select new ExpencesTypeModel
                            {
                                typeID = i.TypeId,
                                typeName = i.Typename
                            }).ToList();
                return item;
            }
        }

        public void NewCategory(string name, int typeid)
        {
            using (Entities db = new Entities())
            {
                Category cat = new Category();
                try
                {
                    cat.CatName = name;
                    cat.TypeID = typeid;
                    db.Category.Add(cat);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void NewSubCategory(string name, int catid)
        {
            using (Entities db = new Entities())
            {
                Subcategory sub = new Subcategory();
                try
                {
                    sub.SubCatname = name;
                    sub.CatID = catid;
                    db.Subcategory.Add(sub);
                    db.SaveChanges();
                }
                catch (Exception x)
                {

                }
            }
        }
    }
}