using Bds.TechTest.Models.WebSearchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Models.Services
{
    public class WebSearcherService
    {
        private readonly IQuerySourceService _querySourceService;

        public WebSearcherService(IQuerySourceService querySourceService)
        {
            _querySourceService = querySourceService;
        }

        public async Task<SearchResults> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var searchResults = new SearchResults();
                searchResults.ErrorList.Add("Search term is a required field.");
                return searchResults;
            }

            return await QuerySources(_querySourceService.GetQuerySources(), searchTerm);
        }

        private async Task<SearchResults> QuerySources(List<ISearchEngineQuery> queryEngines, string searchTerm)
        {
            var searchResults = new SearchResults();
            var searchTasks = queryEngines.Select(x => x.Search(searchTerm)).ToList();

            while (searchTasks.Count > 0)
            {
                var result = await Task.WhenAny(searchTasks);
                searchTasks.Remove(result);

                if (result.IsFaulted)
                {
                    searchResults.ErrorList.Add(result.Exception.InnerException.Message);
                    continue;
                }

                foreach (var r in result.Result)
                    searchResults.AddSearchItem(r);
            }

            return searchResults;
        }
    }
}
