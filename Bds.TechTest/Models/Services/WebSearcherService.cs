using Bds.TechTest.Models.WebSearchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Models.Services
{
    public class WebSearcherService
    {
        private List<SearchEngineQuery> webSearchers = new List<SearchEngineQuery> {
            new GoogleSearcher(),
            new BingSearcher()
        };

        public async Task<SearchResults> Search(string searchTerm)
        {
            var searchResults = new SearchResults();
            var searchResultItems = new List<SearchResultItem>();
            var searchTasks = webSearchers.Select(x => x.Search(searchTerm)).ToList();

            while (searchTasks.Count > 0)
            {
                var result = await Task.WhenAny(searchTasks);
                searchTasks.Remove(result);

                if (result.IsFaulted)
                {
                    searchResults.ErrorList.Add(result.Exception.InnerException.Message);
                    continue;
                }

                searchResultItems.AddRange(result.Result);
            }

            searchResults.Results = searchResultItems;
            return searchResults;
        }
    }
}
