using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bds.TechTest.Models.ViewModels
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            SearchResults = new List<SearchResultItem>();
            Errors = new List<string>();
        }

        [Display(Name = "Name")]
        public string SearchTerm { get; set; }

        public IEnumerable<SearchResultItem> SearchResults { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public bool HasSearchResults
        {
            get
            {
                return SearchResults.Count() > 0;
            }
        }

        public bool HasErrors
        {
            get
            {
                return Errors.Count() > 0;
            }
        }
    }
}
