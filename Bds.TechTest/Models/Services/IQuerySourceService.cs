using Bds.TechTest.Models.WebSearchers;
using System.Collections.Generic;

namespace Bds.TechTest.Models.Services
{
    public interface IQuerySourceService
    {
        List<ISearchEngineQuery> GetQuerySources();
    }
}
