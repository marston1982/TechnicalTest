using System.Collections.Generic;
using System.Linq;

namespace Bds.TechTest.Models
{
    public class SearchResults
    {
        private readonly Dictionary<string, SearchResultItem> searchResults;

        public SearchResults()
        {
            ErrorList = new List<string>();
            searchResults = new Dictionary<string, SearchResultItem>();
        }

        public IEnumerable<SearchResultItem> Results
        {
            get
            {
                return searchResults.Select((k, i) => k.Value);
            }
        }

        public List<string> ErrorList { get; }

        public void AddSearchItem(SearchResultItem item)
        {
            if (searchResults.ContainsKey(item.Link))
                searchResults[item.Link] = searchResults[item.Link].Combine(item);
            else
                searchResults.Add(item.Link, item);
        }
    }
}
