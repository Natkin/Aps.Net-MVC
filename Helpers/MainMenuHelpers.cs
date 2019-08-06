using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcSiteMapProvider.Web.Html.Models;
using System.Web;

namespace Tools.Helpers
{
    public static class MainMenuHelpers
    {
        static UrlHelper urlHelper = null;
        public static MvcHtmlString MainMenu(this HtmlHelper htmlHelper, List<SiteMapNodeModel> nodes)
        {

            urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            StringBuilder html = new StringBuilder();
            foreach (var node in nodes)
            {

                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                if (node.Children.Count==0)
                {
                    var url = urlHelper.Action(node.Action, node.Controller, new { area = node.Area });
                    a.MergeAttribute("href", url);
                }
                else
                {
                    a.MergeAttribute("href", "#");
                }

                 
                   
             

                TagBuilder i = new TagBuilder("i");
                try
                {
                    i.AddCssClass(node.Attributes["ImageCl"].ToString());

                }
                catch
                {

                }

             

                TagBuilder span_text = new TagBuilder("span");
                span_text.SetInnerText(node.Title);
             

                a.InnerHtml = i.ToString();
                a.InnerHtml += span_text.ToString();

                if (node.Children.Count>0)
                {
                    TagBuilder span_arrow = new TagBuilder("span");
                    span_arrow.AddCssClass("fa arrow");
                    a.InnerHtml += span_arrow.ToString();
                }
              
                li.InnerHtml = a.ToString();






                string child_html = CreateChildMenu(node, 0).ToString();
                if (!(string.IsNullOrEmpty(child_html)))
                {

                    li.InnerHtml += child_html;
                }


                html.Append(li.ToString());
            }

            return MvcHtmlString.Create(html.ToString());
        }

        private static MvcHtmlString CreateChildMenu(SiteMapNodeModel node, int level)
        {
            level++;
            string li_all = string.Empty;
            var children = node.Children;
            foreach (var child in children)
            {

                TagBuilder li = new TagBuilder("li");
                if (child.Children.Count > 0)
                {
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", "#");
                    TagBuilder i = new TagBuilder("i");
                    if (child.Attributes["ImageCl"]!=null)
                    {
                        i.AddCssClass(child.Attributes["ImageCl"].ToString());
                    }
                    TagBuilder span_text = new TagBuilder("span");
                    span_text.SetInnerText(child.Title);
                 

                    a.InnerHtml = i.ToString();
                    a.InnerHtml += span_text.ToString();
                    if (child.Children.Count > 0)
                    {
                        TagBuilder span_arrow = new TagBuilder("span");
                        span_arrow.AddCssClass("fa arrow");
                        a.InnerHtml += span_arrow.ToString();
                    }




                    li.InnerHtml += a.ToString();
                    string child_html = CreateChildMenu(child, level).ToString();
                    if (!(string.IsNullOrEmpty(child_html)))
                    {

                        li.InnerHtml += child_html;

                    }
                }
                else
                {
                 
                    TagBuilder a = new TagBuilder("a");
                    a.MergeAttribute("href", urlHelper.Action(child.Action, child.Controller, new { area = child.Area }));
                    a.SetInnerText(child.Title);
                    li.InnerHtml += a.ToString();
                }
              
              
                li_all += li.ToString();
            }
            if (string.IsNullOrEmpty(li_all))
            {
                return new MvcHtmlString(string.Empty);
            }

            TagBuilder ul = new TagBuilder("ul");
            if (level==1)
                ul.AddCssClass("nav nav-second-level");
            else
                ul.AddCssClass("nav nav-third-level");
            ul.InnerHtml = li_all;
            return new MvcHtmlString(ul.ToString());
        }
    }
}
