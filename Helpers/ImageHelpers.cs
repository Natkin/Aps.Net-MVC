using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tools.Helpers
{
    public static class ImageHelpers
    {
    
        public static MvcHtmlString Images(this HtmlHelper htmlHelper, string gender, string SAMAccountName, string style,  string imageclass)
        {
          
            var urlHelper = ((Controller)htmlHelper.ViewContext.Controller).Url;
            string  photoUrl = string.Empty;
       
            photoUrl = urlHelper.Action("avatar", "Base", new { area = "", name = SAMAccountName });
         

            var img = new TagBuilder("img");
            img.MergeAttribute("src", photoUrl);
            img.Attributes.Add("class", imageclass);
            img.Attributes["style"] = style;
            //img.MergeAttribute("alt", alt);
            return new MvcHtmlString(img.ToString());
        }
    }
}
