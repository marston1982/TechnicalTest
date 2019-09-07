using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bds.TechTest.Models.WebSearchers
{
    public interface ISearchEngineQuery
    {
        Task<IEnumerable<SearchResultItem>> Search(string searchTerm);
    }
}
