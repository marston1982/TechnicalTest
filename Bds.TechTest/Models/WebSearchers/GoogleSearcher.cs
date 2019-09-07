using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bds.TechTest.Models.WebSearchers
{
    public class GoogleSearcher : SearchEngineQuery
    {
        private const string Source = "Google";

        protected override string GetSourceName()
        {
            return "Google";
        }

        protected override string GetEngineUrl(string searchTerm)
        {
            return $"http://www.google.com/search?q={searchTerm}";
        }

        protected override IEnumerable<SearchResultItem> ParseResults(Stream htmlStream)
        {
            var doc = new HtmlDocument();
            doc.Load(htmlStream);

            var qwe = doc
                .DocumentNode
                .SelectNodes("//html");

            return doc
                .DocumentNode
                .SelectNodes("//div[@class='BNeawe vvjwJb AP7Wnd']")
                .Select(x => new SearchResultItem(x.InnerText, ProcessUrl(x.Ancestors("a").First().Attributes["href"].Value), GetSourceName()));
        }

        /// <summary> Google adds extra parameters before and after the URL that we want.  I believe the actual URLs are
        /// extracted by JavaScript on Googles end but as we are screen scraping, no JS is run.  </summary>
        /// <param name="url"> The full URL from the screen scrape. </param>
        /// <returns> The actual URL for the search result. </returns>
        private string ProcessUrl(string url)
        {
            var urlEnd = url.IndexOf("&amp;sa") - 7;
            return url.Substring(7, urlEnd);
        }
    }
}
