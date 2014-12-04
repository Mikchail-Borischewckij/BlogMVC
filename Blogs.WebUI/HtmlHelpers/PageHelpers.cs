using Blogs.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blogs.WebUI.HtmlHelpers
{
    public static class PageHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo padingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= padingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == padingInfo.CurrentPage)
                    tag.AddCssClass("btn btn-primary");
                else tag.AddCssClass("btn btn-inverse");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}