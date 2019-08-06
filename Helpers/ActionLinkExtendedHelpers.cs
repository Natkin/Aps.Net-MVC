using DataModels.SingleWindowModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tools.Helpers
{
    public static class ActionLinkExtendedHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionLinkExt(this HtmlHelper htmlHelper,
                                            MenuDropDownModel item, int id, int idh)
        {

            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            TagBuilder  builder = new TagBuilder("a");
            builder.InnerHtml = item.MenuName;

            if (id != 0) { builder.Attributes["href"] = urlHelper.Action(item.Action, item.Controller, new { area = item.Area, id = id }); } else {
            builder.Attributes["href"] = urlHelper.Action(item.Action, item.Controller, new { area = item.Area, id = id, idh = idh }); }
            if (item.Attribute!=null) builder.Attributes.Add(item.Attribute, "");
           // builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }
    }


     
}
