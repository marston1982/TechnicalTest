using Bds.TechTest.Models;
using NUnit.Framework;
using System.Linq;

namespace Bds.TechTest.Tests
{
    public class SearchResultsTests
    {
        [Test]
        public void When_adding_search_items_duplcate_urls_are_combined()
        {
            var results = new SearchResults();
            results.AddSearchItem(new SearchResultItem("the title", "www.hereismylink.com", "Google"));
            results.AddSearchItem(new SearchResultItem("the title", "www.hereismylink.com", "Bing"));

            Assert.AreEqual(1, results.Results.Count());
            Assert.AreEqual(2, results.Results.First().Source.Count);
        }

        [Test]
        public void When_different_links_are_added_multiple_results_are_returned()
        {
            var results = new SearchResults();
            results.AddSearchItem(new SearchResultItem("the title", "www.hereismylink.com", "Google"));
            results.AddSearchItem(new SearchResultItem("the title", "www.anothercoolwebsite", "Google"));

            Assert.AreEqual(2, results.Results.Count());
        }

        [Test]
        public void When_no_search_results_have_been_added_an_empty_list_is_returned_by_results()
        {
            var results = new SearchResults();

            Assert.AreEqual(0, results.Results.Count());
        }
    }
}