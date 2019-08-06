using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tools.Helpers
{


    public static class PaddingPageHelper
    {
        public static int number_pagination_link_on_page = 15;
        public static int number_border_link = 3;
        public static bool nextlast_visible = true;
        static TagBuilder Pagination_ul;


        public static MvcHtmlString Pagination(this HtmlHelper html, PagerInfoClass PagerInfo)//this HtmlHelper html,
        {
            return Pagination(html, PagerInfo, "", 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="PagerInfo"></param>
        /// <param name="ItemPerPageArray">from ItemPerPage property in web.config or string "10,20,50,100"   /></param>
        ///  <param name="ItemPerPage_For">from ItemPerPage property set name of list    /></param>
        /// <returns></returns>
        public static MvcHtmlString Pagination(this HtmlHelper html, PagerInfoClass PagerInfo, string ItemPerPageArray, int CurrentPagesValue, string ItemPerPage_For = "ItemPerPage")//this HtmlHelper html,
        {


            TagBuilder row = new TagBuilder("div");
            row.AddCssClass("row");
            row.InnerHtml += PaginationScripts(PagerInfo, ItemPerPage_For);

            TagBuilder col_pagination = new TagBuilder("div");
            col_pagination.AddCssClass("col-md-10");


            Pagination_ul = new TagBuilder("ul");
            Pagination_ul.AddCssClass("pagination");

            //if (nextlast_visible)
            AddLast(PagerInfo);
          

            /*1 поле для текущей страницы
            1 поле для ....
            */
            int indent = (number_pagination_link_on_page - (number_border_link * 2 + 1) - 1) / 2;



            if ((PagerInfo.CurrentPage > (number_pagination_link_on_page - 1) / 2) && (PagerInfo.CurrentPage < PagerInfo.TotalPages - ((number_pagination_link_on_page - 1) / 2)))
            {

                FragmentPagination(1, number_border_link + 1, PagerInfo.CurrentPage);                            // 3

                FragmentPagination(1, 1, PagerInfo.CurrentPage, "...");
                // 1 
                FragmentPagination(PagerInfo.CurrentPage - indent, PagerInfo.CurrentPage + indent, PagerInfo.CurrentPage);

                FragmentPagination(1, 1, PagerInfo.CurrentPage, "...");                                      // 1 
                FragmentPagination(PagerInfo.TotalPages - number_border_link, PagerInfo.TotalPages, PagerInfo.CurrentPage);     // 3


            }
            else
            {

                if (PagerInfo.CurrentPage < number_pagination_link_on_page - number_border_link)
                {
                    int _pages = PagerInfo.TotalPages > number_pagination_link_on_page - number_border_link ? number_pagination_link_on_page - number_border_link : PagerInfo.TotalPages;
                    FragmentPagination(1, _pages, PagerInfo.CurrentPage);
                    if (PagerInfo.TotalPages > number_pagination_link_on_page - number_border_link)
                    {
                        FragmentPagination(1, 1, PagerInfo.CurrentPage, "...");
                        FragmentPagination(PagerInfo.TotalPages - number_border_link, PagerInfo.TotalPages, PagerInfo.CurrentPage);
                    }
                }
                else if (PagerInfo.CurrentPage > PagerInfo.TotalPages - number_pagination_link_on_page - number_border_link)
                {
                    FragmentPagination(1, number_border_link + 1, PagerInfo.CurrentPage);
                    FragmentPagination(1, 1, PagerInfo.CurrentPage, "...");
                    FragmentPagination(PagerInfo.TotalPages - number_pagination_link_on_page + number_border_link, PagerInfo.TotalPages, PagerInfo.CurrentPage);
                }
            }

            //if (nextlast_visible)
            AddNext(PagerInfo);
            col_pagination.InnerHtml += Pagination_ul.ToString();

            TagBuilder col_perpage = new TagBuilder("div");
            col_perpage.AddCssClass("col-md-2");

            if (ItemPerPageArray.Length > 0)
            {
                col_perpage.InnerHtml += ItemPerPage(ItemPerPageArray, CurrentPagesValue);

            }
            row.InnerHtml += col_pagination.ToString();
            row.InnerHtml += col_perpage.ToString();

            return new MvcHtmlString(row.ToString());
        }


        private static void AddNext(PagerInfoClass PagerInfo)
        {
            TagBuilder item_li = new TagBuilder("li");
            item_li.AddCssClass("page");
            item_li.AddCssClass("page");

            TagBuilder item_tag = new TagBuilder("a");
            item_tag.MergeAttribute("href", "#");

            item_tag.SetInnerText(">");
            if ((PagerInfo.CurrentPage + 1) < PagerInfo.TotalPages)
                item_tag.Attributes.Add("goto_page", (PagerInfo.CurrentPage + 1).ToString());
            else
                item_tag.Attributes.Add("goto_page", PagerInfo.TotalPages.ToString());

            item_li.InnerHtml = item_tag.ToString();
            Pagination_ul.InnerHtml += item_li.ToString();




            item_tag = new TagBuilder("a");
            item_tag.MergeAttribute("href", "#");

            item_tag.SetInnerText(">>>");
            item_tag.Attributes.Add("goto_page", PagerInfo.TotalPages.ToString());
            item_li.InnerHtml = item_tag.ToString();

            Pagination_ul.InnerHtml += item_li.ToString();



        }

        private static void AddLast(PagerInfoClass PagerInfo)
        {

            TagBuilder item_li = new TagBuilder("li");

            TagBuilder item_tag = new TagBuilder("a");
            item_tag.MergeAttribute("href", "#");
            item_tag.SetInnerText("<<<");
            item_tag.Attributes.Add("goto_page", "1");

            item_li.InnerHtml = item_tag.ToString();
            Pagination_ul.InnerHtml += item_li.ToString();


            item_tag = new TagBuilder("a");
            item_tag.MergeAttribute("href", "#");
            item_tag.SetInnerText("<");
            if ((PagerInfo.CurrentPage - 1) > 1)
                item_tag.Attributes.Add("goto_page", (PagerInfo.CurrentPage - 1).ToString());
            else
                item_tag.Attributes.Add("goto_page", "1");
            item_li = new TagBuilder("li");
            item_li.InnerHtml = item_tag.ToString();
            Pagination_ul.InnerHtml += item_li.ToString();

        }


        public static void FragmentPagination(int from, int to, int current_page, string custom = "")
        {

            if (custom.Length > 0)
            {

                TagBuilder item_li = new TagBuilder("li");
                item_li.AddCssClass("disabled");
                item_li.AddCssClass("hidden-xs");


                TagBuilder item_tag = new TagBuilder("span");
                item_tag.SetInnerText(custom);

                item_li.InnerHtml += item_tag.ToString();
                Pagination_ul.InnerHtml += item_li.ToString();



            }
            else
            {
                for (int i = from; i <= to; i++)
                {

                    if (i != current_page)
                    {
                        TagBuilder item_li = new TagBuilder("li");
                        item_li.AddCssClass("hidden-xs");
                        TagBuilder item_tag = new TagBuilder("a");
                        item_tag.MergeAttribute("href", "#");
                        item_tag.SetInnerText(i.ToString());

                        TagBuilder item_span = new TagBuilder("span");
                        item_span.AddCssClass("sr-only");
                        item_span.SetInnerText("(current)");

                        item_li.InnerHtml += item_tag.ToString();
                        Pagination_ul.InnerHtml += item_li.ToString();
                    }
                    else
                    {
                        TagBuilder item_li = new TagBuilder("li");
                        item_li.AddCssClass("active");


                        TagBuilder item_tag = new TagBuilder("span");
                        item_tag.MergeAttribute("href", "#");
                        item_tag.SetInnerText(i.ToString());

                        item_li.InnerHtml += item_tag.ToString();
                        Pagination_ul.InnerHtml += item_li.ToString();
                    }
                }
            }
        }


        public static MvcHtmlString ItemPerPage(string ItemPerPageArray, int currentvalue)
        {


            TagBuilder select = new TagBuilder("select");
            select.AddCssClass("form-control pagination");
            select.Attributes.Add("id", "item_per_page");
           


            List<int> ar = GetDigitalItemPerPage(ItemPerPageArray);

            foreach (int p in ar)
            {

                TagBuilder option = new TagBuilder("option");
                
                option.MergeAttribute("value", p.ToString());
                option.SetInnerText(p.ToString());
                if (p == currentvalue)
                {
                    option.MergeAttribute("selected", "selected");
                   
                }
                select.InnerHtml += option.ToString();
            }



            return new MvcHtmlString(select.ToString());
        }


        public static List<int> GetDigitalItemPerPage(string ItemPerPageArray)
        {
            List<int> dig = new List<int>();
            string[] s = ItemPerPageArray.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in s)
            {
                int d;
                int.TryParse(item, out d);
                dig.Add(d);
            }

            return dig;
        }
        public static string PaginationScripts(PagerInfoClass PagerInfo, string ItemPerPage_For)
        {
            TagBuilder scriptTag = new TagBuilder("script");
          //  scriptTag.MergeAttribute("type", "message_text/javascript");

            StringBuilder sb = new StringBuilder();
            
            //решить вопрос 1 запуском странички, с индекс страницы, когда goto_page еще не существует

            //scriptTag.InnerHtml += "$(function () {" +
            //    "$(document.body).on('click', '.pagination li', function () {" +
            //    " var page = parseInt($(this).find('a').html());" +
            //    " var getvalue = $(this).find('a').attr('goto_page');" +
            //    " if (getvalue > 0)" +
            //    "  page = getvalue;" +
            //    " goto_page(page, false);" +
            //   " });" +
            //    "});"
            //    ;

            //scriptTag.InnerHtml += "function goto_page(page, isClear) {"
            //  + " var filter = GetFilter(isClear);"
            //  + " $.ajax({"
            //  + " type: 'post',"
            //  + "url: $('#list').data('urllist'),"
            //  + "data: { 'filter': JSON.stringify(filter) },"
            //  + "success: function (data) { "
            //  + "   $('#list').html(data);    },"
            //  + "error: function () {"
            //  + "   alert('Серверна помилка !');"
            //  + "}    });}"
            //  ;

            //Отключил , потому что вызывается по несколько раз . Видимо потому что скрипт как то прописывается в браузере и срабатыывает событие 'change', '#item_per_page' многократно
         //   scriptTag.InnerHtml += "$(document.body).on('change', '#item_per_page', function () { localStorage['" + ItemPerPage_For + "'] = $(this).val();    goto_page(1, false,callback_goto_page);});";
         //   scriptTag.InnerHtml += "$(document ).ready(function () { if (localStorage['" + ItemPerPage_For + "'] == null) { localStorage['" + ItemPerPage_For + "'] = localStorage['ItemPerPage']; }  $('#item_per_page option').filter(function() { return $(this).message_text() == localStorage['" + ItemPerPage_For + "']; }).prop('selected', true); });";
           

            return scriptTag.ToString();
        }
       

    }
}


//$(function () {
//    $(document.body).on('click', '.pagination li', function () {

//        var page = parseInt($(this).find("a").html());
//        var getvalue = $(this).find('a').attr('goto_page');

//        if (getvalue > 0)
//            page = getvalue;
//        goto_page(page, false);

//    });
//});

//function goto_page(page, isClear) {
//    var filter = GetFilter(isClear);

//    $.ajax({
//        type: 'post',
//        url: $('#list').data('urllist'),
//        data: { 'filter': JSON.stringify(filter) },
//        success: function (data) {
//            $('#list').html(data);
//        },
//        error: function () {
//            alert("Серверна помилка !");
//        }
//    });
//}