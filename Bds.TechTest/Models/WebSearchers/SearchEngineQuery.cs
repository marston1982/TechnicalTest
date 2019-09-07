using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bds.TechTest.Models.WebSearchers
{
    public abstract class SearchEngineQuery : ISearchEngineQuery
    {
        public async Task<IEnumerable<SearchResultItem>> Search(string searchTerm)
        {
            var hc = new HttpClient();

            try
            {
                var result = await hc.GetAsync(GetEngineUrl(searchTerm));
                var resultStream = await result.Content.ReadAsStreamAsync();
                return ParseResults(resultStream);
            }
            catch (Exception ex)
            {
                throw new Exception($"{GetSourceName()} Failed.", ex);
            }
        }

        protected abstract string GetSourceName();

        protected abstract IEnumerable<SearchResultItem> ParseResults(Stream htmlStream);

        protected abstract string GetEngineUrl(string searchTerm);
    }
}
