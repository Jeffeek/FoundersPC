#region Using namespaces

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

#endregion

namespace FoundersPC.Web.TagHelpers
{
    [HtmlTargetElement("check-indeterminate")]
    public class InputTypeWithIndeterminateTagHelper : TagHelper
    {
        [HtmlAttributeName("type")]
        public string Type { get; set; } = "text";

        [HtmlAttributeName("disabled")]
        public bool IsDisabled { get; set; } = false;

        [HtmlAttributeName("indeterminate")]
        public bool IsIndeterminate { get; set; } = false;

        [HtmlAttributeName("checked")]
        public bool IsChecked { get; set; }

        #region Overrides of TagHelper

        /// <inheritdoc/>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.TagMode = TagMode.StartTagAndEndTag;

            var input = new TagBuilder("input")
                        {
                            TagRenderMode = TagRenderMode.SelfClosing
                        };

            input.Attributes.Add("type", Type);
            input.AddCssClass("form-check-input");
            Disabled(input);
            Indeterminate(input);
            Checked(input);

            output.Content.AppendHtml(input);
        }

        #endregion

        private void Checked(TagBuilder builder)
        {
            if ((Type == "checkbox" || Type == "radio") && IsChecked)
                builder.Attributes.Add("checked", "true");
        }

        private void Disabled(TagBuilder builder)
        {
            if (IsDisabled)
                builder.Attributes.Add("disabled", "true");
        }

        private void Indeterminate(TagBuilder builder)
        {
            if ((Type == "checkbox" || Type == "radio") && IsIndeterminate)
                builder.Attributes.Add("indeterminate", "true");
        }
    }
}