using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bds.TechTest.Models.WebSearchers
{
    public class BingSearcher : SearchEngineQuery
    {
        protected override string GetSourceName()
        {
            return "Bing";
        }

        protected override string GetEngineUrl(string searchTerm)
        {
            return $"https://www.bing.com/search?q={searchTerm}";
        }

        protected override IEnumerable<SearchResultItem> ParseResults(Stream htmlStream)
        {
            var doc = new HtmlDocument();
            doc.Load(htmlStream);

            return doc
                .DocumentNode
                .SelectNodes("//li[@class='b_algo']")
                .Select(x =>
                {
                    var container = x.SelectSingleNode(".//h2");
                    return new SearchResultItem(container.InnerText, container.FirstChild.Attributes["href"].Value, GetSourceName());
                });
        }
    }
}
