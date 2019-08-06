using DataModels.WokrStation.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tools.Helpers
{
    public static class SpecificationPartitionHelper
    {

        public static MvcHtmlString Partition(this HtmlHelper html, SpecificationPartitionModel PartitionModel, int index)//this HtmlHelper html,
        {
            TagBuilder tag = new TagBuilder("div");
            TagBuilder scriptTag = new TagBuilder("script");
            TagBuilder divhidden = new TagBuilder("div");

            TagBuilder PartitionID = new TagBuilder("input");
            PartitionID.MergeAttribute("type", "hidden");
            PartitionID.MergeAttribute("id", string.Format("SpecificationPartitions_{0}__PartitionID", index));
            PartitionID.MergeAttribute("name", string.Format("SpecificationPartitions[{0}].PartitionID", index));
            PartitionID.MergeAttribute("value", PartitionModel.PartitionID.ToString());




            divhidden.InnerHtml += PartitionID;

            string html_string_template = PartitionModel.PartitionHtmlTemplate;
            int param_index = 0;
            foreach (var itemvalue in PartitionModel.PartionValues)
            {
                //добавить скипты для парамерта в html
                if (!string.IsNullOrEmpty(itemvalue.PartitionValueScript))
                {
                    scriptTag.InnerHtml += itemvalue.arrayvalues;
                    //формирование скрипта для каждого значения
                    if (!string.IsNullOrEmpty(itemvalue.defaultvalue))
                    {
                        if (string.IsNullOrEmpty(itemvalue.Value))
                        {
                            scriptTag.InnerHtml += string.Format(itemvalue.PartitionValueScript, itemvalue.defaultvalue);
                        }
                        else
                        {
                            scriptTag.InnerHtml += string.Format(itemvalue.PartitionValueScript, itemvalue.Value);
                        }
                    }
                    else
                    {
                        scriptTag.InnerHtml += itemvalue.PartitionValueScript;
                    }
               

                    var id = string.Format("SpecificationPartitions_{0}__PartionValues_{1}__Value", index, param_index);

                    string save = "$('#" + itemvalue.InputID + "').on('save', function(e, params) {$('#" + id + "').val(params.newValue);});";
                    scriptTag.InnerHtml += save;
                }

                //создание контрола который будет виден и вводится информация в него
                TagBuilder v_tag = new TagBuilder(itemvalue.TagName);
                v_tag.MergeAttribute("id", itemvalue.InputID);
                v_tag.MergeAttribute("class", itemvalue.TagClassCss);

                if (!string.IsNullOrEmpty(itemvalue.TagStyle))
                    v_tag.MergeAttribute("style", itemvalue.TagStyle);

                if (!string.IsNullOrEmpty(itemvalue.PlaceHolder))
                    v_tag.MergeAttribute("placeholder", itemvalue.PlaceHolder);

                if (itemvalue.DataAttribute != null)
                {
                    foreach (var attr in itemvalue.DataAttribute)
                    {
                        v_tag.MergeAttribute("data-" + attr.key, attr.value);
                    }
                }
                v_tag.SetInnerText(itemvalue.Value);

                //если нужно создаем промежуточный контроли для хранения информации
                if (itemvalue.CreateServisesInput)
                {
                    TagBuilder input = new TagBuilder("input");
                    input.MergeAttribute("type", "hidden");
                    input.MergeAttribute("id", string.Format("SpecificationPartitions_{0}__PartionValues_{1}__ID", index, param_index));
                    input.MergeAttribute("name", string.Format("SpecificationPartitions[{0}].PartionValues[{1}].ValueID", index, param_index));
                    input.MergeAttribute("value", itemvalue.ValueID.ToString());

                    divhidden.InnerHtml += input.ToString();

                    input = new TagBuilder("input");
                    input.MergeAttribute("type", "hidden");
                    input.MergeAttribute("id", string.Format("SpecificationPartitions_{0}__PartionValues_{1}__Value", index, param_index));
                    input.MergeAttribute("name", string.Format("SpecificationPartitions[{0}].PartionValues[{1}].Value", index, param_index));
                    input.MergeAttribute("value", itemvalue.Value);


                    divhidden.InnerHtml += input.ToString();
                }
                else //созданому контролу присвоить id и name для привязки модели
                {
                    TagBuilder input = new TagBuilder("input");
                    input.MergeAttribute("type", "hidden");
                    input.MergeAttribute("id", string.Format("SpecificationPartitions_{0}__PartionValues_{1}__ID", index, param_index));
                    input.MergeAttribute("name", string.Format("SpecificationPartitions[{0}].PartionValues[{1}].ValueID", index, param_index));
                    input.MergeAttribute("value", itemvalue.ValueID.ToString());

                    divhidden.InnerHtml += input.ToString();

                    v_tag.MergeAttribute("id", string.Format("SpecificationPartitions_{0}__PartionValues_{1}__Value", index, param_index));
                    v_tag.MergeAttribute("name", string.Format("SpecificationPartitions[{0}].PartionValues[{1}].Value", index, param_index));
                }


                html_string_template = html_string_template.Replace(itemvalue.InputID, v_tag.ToString());

                param_index++;
            }
            tag.InnerHtml += divhidden;
            tag.InnerHtml += html_string_template;
            tag.InnerHtml += scriptTag;


            return new MvcHtmlString(tag.ToString());


        }
    }
}
