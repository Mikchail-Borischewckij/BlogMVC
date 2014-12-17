using System;
using System.Text;
using System.Web.Mvc;
using Blogs.WebUI.Models;

namespace Blogs.WebUI.HtmlHelpers
{
    public static class PageHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo padingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (int i = 1; i <= padingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == padingInfo.CurrentPage)
                    tag.AddCssClass("btn btn-primary");
                else tag.AddCssClass("btn btn-inverse");
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}