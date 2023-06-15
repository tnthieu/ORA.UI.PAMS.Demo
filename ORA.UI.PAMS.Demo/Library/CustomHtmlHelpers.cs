using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ORA.UI.PAMS.Demo.Library
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent ImageActionLink(this IHtmlHelper htmlHelper,
            string linkText, string action, string controller,
            object routeValues, object htmlAttributes, string imageSrc)
        {
            if (imageSrc.StartsWith("~"))
                imageSrc = imageSrc.Remove(0, 1);

            using (var writer = new StringWriter())
            {
                htmlHelper.ActionLink("@Image@", action, controller, routeValues, htmlAttributes)
                    .WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

                var actionLinkHtml = writer.ToString();
                actionLinkHtml = actionLinkHtml.Replace("@Image@", "<img src='" + imageSrc + "'>");

                return htmlHelper.Raw(actionLinkHtml);
            }
        }
    }
}
