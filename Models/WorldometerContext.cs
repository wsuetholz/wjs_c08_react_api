
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace wjs_c08_react_api.Models
{
    public class WorldometerContext
    {
        static ScrapingBrowser _scrapingbrowser = new ScrapingBrowser();

        static HtmlNode GetHtml(string url)
        {
            WebPage webPage = _scrapingbrowser.NavigateToPage(new Uri(url));
            return webPage.Html;
        }
    }

}
