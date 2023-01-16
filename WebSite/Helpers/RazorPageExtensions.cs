using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Policy;

namespace WebSite.Helpers
{
    public static class MvcExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string actionName, 
            string find, string page = "nothing", string cssClass = "active")
        {

            if (actionName.StartsWith("/Index") && page == "home")
            {
                return cssClass;
            }


            if (actionName.Contains(find) && page != "home") {
                return cssClass;
            }

            return string.Empty;

        }
    }
}
