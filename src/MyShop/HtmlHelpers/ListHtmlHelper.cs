using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyShop1.HtmlHelpers
{
    public static class ListHtmlHelpers
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items) 
        {
            var result = "<p>Our List:</p><ul>";
            foreach (var item in items)
            {
                result += $"<li>{item}</li>";
            }
            result += "</ul>";

            return new HtmlString(result);
        }
    }
}
