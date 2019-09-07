using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bds.TechTest.Models
{
    public class SearchResults
    {
        public SearchResults()
        {
            ErrorList = new List<string>();
        }

        public IEnumerable<SearchResultItem> Results { get; set; }
        public List<string> ErrorList { get; set; }
    }
}
