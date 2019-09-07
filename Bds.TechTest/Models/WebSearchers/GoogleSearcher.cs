using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

            // Gets the link google uses but it has google appended stuff
            // x.Ancestors("a").First().Attributes["href"].Value

            return doc
                .DocumentNode
                .SelectNodes("//div[@class='BNeawe vvjwJb AP7Wnd']")
                .Select(x => new SearchResultItem(x.InnerText, x.NextSibling.InnerText, GetSourceName()));
        }
    }
}
