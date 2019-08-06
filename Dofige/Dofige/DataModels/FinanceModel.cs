using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dofige.DataModels
{
    public class CategoryModel
    {
        public int CatID { get; set; }
        public string CatName { get; set; }
        public ExpencesTypeModel TypeModel { get; set; }
        public SubCategoryModel Subcategory { get; set; }
        public List<SubCategoryModel> Subcategories { get; set; }
    }
    public class SubCategoryModel
    {
        public int SubCatID { get; set; }
        public string SubCatName { get; set; }
    }
    public class ExpencesTypeModel
    {
        public int typeID { get; set; }
        public string typeName { get; set; }
    }
    public class FinanceModel
    {
        public int finID { get; set; }
        [Required]
        public decimal amount { get; set; }
        public DateTime data { get; set; }
        public CategoryModel Category { get; set; }
    }
    public class FilterModel
    {
        public DateTime? start_data { get; set; }
        public DateTime? end_data { get; set; }       
        public int? typeID { get; set; }
        public int? subcatid { get; set; }
        public int? current_page { get; set; }
        public int? catid { get; set; }
    }
    public class PagerModel
    {
        public int Items_per_page { get; set; }
        public int Totalitems { get; set; }
        public List<FinanceModel> Finance { get; set; }
        public FilterModel Filter { get; set; }
        public int TotalPages { get; set; }
        
    }
}