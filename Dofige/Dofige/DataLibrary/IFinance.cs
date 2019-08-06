using Dofige.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dofige.DataLibrary
{
    public interface IFinance
    {
        PagerModel GetListdata(FilterModel filter);
        void Create(PagerModel model);
        List<CategoryModel> GetFinanceTypeList();
        List<SubCategoryModel> GetSubCat();
        List<ExpencesTypeModel> GetAllTypes();
        void NewCategory(string name, int typeid);
        void NewSubCategory(string name, int catid);
    }
}
