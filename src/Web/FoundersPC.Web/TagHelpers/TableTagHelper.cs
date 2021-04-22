using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FoundersPC.Web.TagHelpers
{
    [HtmlTargetElement("hardware-table")]
    public class TableTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelper;
        private readonly IActionContextAccessor _actionContextAccessor;

        [HtmlAttributeName("asp-controller")]
        public string ControllerName { get; set; }

        [HtmlAttributeName("entity-type")]
        public Type Type { get; set; }

        [HtmlAttributeName("entities")]
        public IEnumerable List { get; set; }

        public TableTagHelper(IUrlHelperFactory urlHelper,
                              IActionContextAccessor actionContextAccessor)
        {
            _urlHelper = urlHelper;
            _actionContextAccessor = actionContextAccessor;
        }

        #region Overrides of TagHelper

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("mt-2", HtmlEncoder.Default);
            output.AddClass("table-wrapper", HtmlEncoder.Default);
            output.AddClass("text-center", HtmlEncoder.Default);

            var wrapper = CreateWrapper();

            output.Content.SetHtmlContent(wrapper);
        }

        #endregion

        private TagBuilder CreateWrapper()
        {
            var wrapper = new TagBuilder("div");

            var title = CreateTableTitle();

            var body = CreateTableWrapper();

            wrapper.InnerHtml.AppendHtml(title);
            wrapper.InnerHtml.AppendHtml(body);

            return wrapper;
        }

        private TagBuilder CreateTableTitle()
        {
            var title = new TagBuilder("div");
            title.AddCssClass("p-2 table-title");

            var rowWrapper = new TagBuilder("div");
            rowWrapper.AddCssClass("row");

            var divWrapper = new TagBuilder("div");

            var anchor = new TagBuilder("a");
            anchor.AddCssClass("btn btn-outline-success d-flex");
            var helper = _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext);
            anchor.Attributes.Add("href", helper.ActionLink("Create", ControllerName));

            var svg = new TagBuilder("svg");
            svg.AddCssClass("icon");
            svg.Attributes.Add("fill", "#FFFFFF");
            svg.Attributes.Add("height", "32");
            svg.Attributes.Add("width", "32");
            svg.Attributes.Add("ViewBox", "0 0 32 32");

            var path = new TagBuilder("path");
            path.Attributes.Add("d", "M16 11L16 21M11 16L21 16M16 4A12 12 0 1 0 16 28 12 12 0 1 0 16 4z");
            path.Attributes.Add("fill", "none");
            path.Attributes.Add("stroke", "#FFFFFF");
            path.Attributes.Add("stroke-width", "2");

            var span = new TagBuilder("span");
            span.AddCssClass("mx-2 text-light");
            span.InnerHtml.SetContent("Add new");

            svg.InnerHtml.AppendHtml(path);
            svg.InnerHtml.AppendHtml(span);

            anchor.InnerHtml.AppendHtml(svg);

            divWrapper.InnerHtml.AppendHtml(anchor);

            rowWrapper.InnerHtml.AppendHtml(divWrapper);

            title.InnerHtml.AppendHtml(rowWrapper);

            return title;
        }

        private TagBuilder CreateTableWrapper()
        {
            var properties = GetProperties()
                .ToArray();

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("table-responsive");

            var table = new TagBuilder("table");
            table.AddCssClass("mt-3 table table-bordered table-hover text-light");

            var thead = new TagBuilder("thead");
            var theadRow = new TagBuilder("tr");

            foreach (var property in properties)
                theadRow.InnerHtml.AppendHtml($"<th>{property}</th>");

            theadRow.InnerHtml.AppendHtml("<th>Actions</th>");

            thead.InnerHtml.AppendHtml(theadRow);

            table.InnerHtml.AppendHtml(thead);

            var tbody = new TagBuilder("tbody");

            foreach (var q in List)
            {
                var row = new TagBuilder("tr");

                foreach (var property in properties)
                {
                    var th = new TagBuilder("th");

                    var content = Type.GetProperty(property)
                                      ?.GetValue(q);

                    if (content is DateTime dt)
                    {
                        th.InnerHtml.SetContent(dt.ToShortDateString());
                    }
                    else
                    {
                        th.InnerHtml.SetContent(content?.ToString() ?? String.Empty);
                    }

                    row.InnerHtml.AppendHtml(th);
                }

                var actionsTd = new TagBuilder("td");
                actionsTd.AddCssClass("d-flex flex-row justify-content-center");

                var editActionWrapper = new TagBuilder("div");
                editActionWrapper.AddCssClass("d-flex");

                var editAnchor = new TagBuilder("a");
                editAnchor.AddCssClass("btn btn-outline-warning d-flex mx-1 simple-popup");

                editAnchor.Attributes.Add("href",
                                          _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext)
                                                    .ActionLink("Edit",
                                                                ControllerName,
                                                                new
                                                                {
                                                                    id = Int32.Parse(Type.GetProperty("Id")
                                                                                         ?.GetValue(q)
                                                                                         ?.ToString()
                                                                                     ?? String.Empty)
                                                                }));

                var popupSpan = new TagBuilder("span");
                popupSpan.AddCssClass("popuptext");
                popupSpan.InnerHtml.SetHtmlContent("Edit");

                var svgHtml =
                    "<svg class='icon' viewBox=\"0 0 48 48\"><path d=\"M42.6,9.1l-3.7-3.7c-0.6-0.6-1.5-0.6-2,0l-1.7,1.7l5.7,5.7l1.7-1.7C43.1,10.5,43.1,9.6,42.6,9.1\"fill=\"#e57373\" /><path d=\"M38,15.6L12.6,41.1l-5.7-5.7L32.4,10L38,15.6z\"fill=\"#ff9800\" /><path d=\"M32.4,10l2.8-2.8l5.7,5.7L38,15.6L32.4,10z\"fill=\"#b0bec5\" /><path d=\"M6.9,35.4L5,43l7.6-1.9L6.9,35.4z\"fill=\"#ffc107\" /><path d=\"M6,39.2L5,43l3.8-1L6,39.2z\"fill=\"#37474f\" /></svg>";

                editAnchor.InnerHtml.AppendHtml(popupSpan);
                editAnchor.InnerHtml.AppendHtml(svgHtml);

                var removeAnchor = new TagBuilder("a");

                removeAnchor.AddCssClass("btn btn-outline-danger d-flex mx-3 simple-popup");

                removeAnchor.Attributes.Add("href",
                                            _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext)
                                                      .ActionLink("Remove",
                                                                  ControllerName,
                                                                  new
                                                                  {
                                                                      id = Int32.Parse(Type.GetProperty("Id")
                                                                                           ?.GetValue(q)
                                                                                           ?.ToString()
                                                                                       ?? String.Empty)
                                                                  }));

                var popupSpan2 = new TagBuilder("span");
                popupSpan2.AddCssClass("popuptext");
                popupSpan2.InnerHtml.SetHtmlContent("Remove");

                var svgHtml2 =
                    "<svg class=\"icon\"fill=\"#FFFFFF\"viewBox=\"0 0 32 32\"><path d=\"M23 27H11c-1.1 0-2-.9-2-2V8h16v17C25 26.1 24.1 27 23 27zM27 8L7 8M14 8V6c0-.6.4-1 1-1h4c.6 0 1 .4 1 1v2M17 23L17 12M21 23L21 12M13 23L13 12\"fill=\"none\"stroke=\"#FFFFFF\"stroke-width=\"2\" /></svg>";

                removeAnchor.InnerHtml.AppendHtml(popupSpan2);
                removeAnchor.InnerHtml.AppendHtml(svgHtml2);

                editActionWrapper.InnerHtml.AppendHtml(editAnchor);
                editActionWrapper.InnerHtml.AppendHtml(removeAnchor);

                actionsTd.InnerHtml.AppendHtml(editActionWrapper);

                row.InnerHtml.AppendHtml(actionsTd);

                tbody.InnerHtml.AppendHtml(row);
            }

            table.InnerHtml.AppendHtml(tbody);

            wrapper.InnerHtml.AppendHtml(table);

            return wrapper;
        }

        private IEnumerable<string> GetProperties()
        {
            return Type.GetProperties()
                       .Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string))
                       .Select(x => x.Name);
        }
    }
}
