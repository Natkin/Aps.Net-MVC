using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tools.Helpers
{

    public class SelectListExtended
    {
        public SelectListExtended(IEnumerable items, string dataValueField, string dataTextField, object dataValueSelected, string dataAttrField, object dataValueSelectedText=null)
        {
            Items = items;
            DataValueField = dataValueField;
            DataTextField = dataTextField;
            DataAttrField = dataAttrField;
            DataValueSelected = dataValueSelected;
            DataValueSelectedText = dataValueSelectedText;
        }
        public IEnumerable Items { get; set; }
        public string DataValueField { get; set; }
        public string DataTextField { get; set; }
        public string DataAttrField { get; set; }
        public object DataValueSelected { get; set; }
        public object DataValueSelectedText { get; set; }
    }

    public static class DropDownListExtendedClass
    {                                                                           
        public static MvcHtmlString DropDownListExtended(this HtmlHelper html, string name, SelectListExtended list, string empty, IDictionary<String, Object> attr)
        {
            string str = "<select{0} name=\"{1}\"  id=\"{1}\"> {2} </select>";

            string attrStrF = " {0}=\"{1}\"";
            string attrStr = "";
            foreach (string k in attr.Keys)
            {
                attrStr += string.Format(attrStrF, k, attr[k]);
            }

            string optionStrF = " <option {0} {1}>{2}</option>";
            string optionStr = "";
            string sel = "selected=\"selected\"";
            if (empty != null)
            {
                optionStr = String.Format(optionStrF, "value", "", empty);
            }
                optionStrF = String.Format(optionStrF, "{0} value=\"{1}\"", "{2}", "{3}");
            

            
            foreach (object i in list.Items)
            {
                System.Reflection.PropertyInfo PIValueField = i.GetType().GetProperty(list.DataValueField);
                System.Reflection.PropertyInfo PITextField = i.GetType().GetProperty(list.DataTextField);
                System.Reflection.PropertyInfo PIDataAttrField = i.GetType().GetProperty(list.DataAttrField);
                object a = PIDataAttrField.GetValue(i);
                string optionAttrStr = "";
                if (a is IEnumerable)
                {
                    string optionAttrStrF = "data-{2}_{0}=\"{1}\"";
                    //List<string> l = a as List<string>;
                    int j = 0;
                    foreach (object s in a as IEnumerable)
                    {
                        optionAttrStr += string.Format(optionAttrStrF, j, s, list.DataAttrField);
                        j++;
                    }
                }
                else
                {
                    optionAttrStr += string.Format("data-{1}=\"{0}\"", PIDataAttrField.GetValue(i), list.DataAttrField);
                }
                optionStr += string.Format(optionStrF, (int)PIValueField.GetValue(i) == (int)list.DataValueSelected || (list.DataValueSelectedText==null ? 1!=1 :  PITextField.GetValue(i).ToString() == list.DataValueSelectedText.ToString()) ? sel : "", PIValueField.GetValue(i), optionAttrStr, PITextField.GetValue(i));
            }

            str = string.Format(str, attrStr, name, optionStr);
            MvcHtmlString res = new MvcHtmlString(str);

            return res;
        }
    }
}
