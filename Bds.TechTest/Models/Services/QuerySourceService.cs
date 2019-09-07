using Bds.TechTest.Models.WebSearchers;
using System.Collections.Generic;

namespace Bds.TechTest.Models.Services
{
    public class QuerySourceService : IQuerySourceService
    {
        private List<ISearchEngineQuery> webSearchers = new List<ISearchEngineQuery> {
            new GoogleSearcher(),
            new BingSearcher()
        };

        public List<ISearchEngineQuery> GetQuerySources()
        {
            return webSearchers;
        }
    }
}
