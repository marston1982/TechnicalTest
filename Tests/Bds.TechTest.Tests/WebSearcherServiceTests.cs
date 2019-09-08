using Bds.TechTest.Models;
using Bds.TechTest.Models.Services;
using Bds.TechTest.Models.WebSearchers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bds.TechTest.Tests
{
    public class WebSearcherServiceTests
    {
        private Mock<IQuerySourceService> _querySearcherService;
        private List<ISearchEngineQuery> _searchers;

        [SetUp]
        public void Setup()
        {
            _searchers = new List<ISearchEngineQuery>();
            _querySearcherService = new Mock<IQuerySourceService>();
            _querySearcherService.Setup(x => x.GetQuerySources()).Returns(_searchers);
        }

        [Test]
        public async Task When_empty_search_term_is_passed_an_error_is_returned()
        {
            var webSearcherService = new WebSearcherService(_querySearcherService.Object);
            var results = await webSearcherService.Search(string.Empty);

            Assert.AreEqual(1, results.ErrorList.Count);
            Assert.AreEqual(0, results.Results.Count());
        }

        [Test]
        public async Task When_null_search_term_is_passed_an_error_is_returned()
        {
            var webSearcherService = new WebSearcherService(_querySearcherService.Object);
            var results = await webSearcherService.Search(null);

            Assert.AreEqual(1, results.ErrorList.Count);
            Assert.AreEqual(0, results.Results.Count());
        }

        [Test]
        public async Task When_searching_any_errors_are_caught_and_handled()
        {
            var q = new Mock<ISearchEngineQuery>();
            q.Setup(x => x.Search(It.IsAny<string>())).Returns(Task.FromException<IEnumerable<SearchResultItem>>(new Exception("Kaboom")));

            _searchers.Add(q.Object);

            var webSearcherService = new WebSearcherService(_querySearcherService.Object);
            var results = await webSearcherService.Search("Test");

            Assert.AreEqual(1, results.ErrorList.Count);
            Assert.AreEqual(0, results.Results.Count());
        }

        [Test]
        public async Task When_searching_successful_results_are_returned()
        {
            IEnumerable<SearchResultItem> searchReturn = new List<SearchResultItem> { new SearchResultItem("Test", "www.test.com", "Google") };

            var q = new Mock<ISearchEngineQuery>();
            q.Setup(x => x.Search(It.IsAny<string>())).Returns(Task.FromResult(searchReturn));

            _searchers.Add(q.Object);

            var webSearcherService = new WebSearcherService(_querySearcherService.Object);
            var results = await webSearcherService.Search("Test");

            Assert.AreEqual(0, results.ErrorList.Count);
            Assert.AreEqual(1, results.Results.Count());
            var searchResult = results.Results.First();
            Assert.AreEqual("Test", searchResult.Title);
        }

        [Test]
        public async Task When_searching_multiple_searchers_and_one_errors_other_searchers_still_complete_and_return_results()
        {
            IEnumerable<SearchResultItem> searchReturn = new List<SearchResultItem> { new SearchResultItem("Test", "www.test.com", "Google") };

            var goodSearcher = new Mock<ISearchEngineQuery>();
            goodSearcher.Setup(x => x.Search(It.IsAny<string>())).Returns(Task.FromResult(searchReturn));
            _searchers.Add(goodSearcher.Object);

            var badSearcher = new Mock<ISearchEngineQuery>();
            badSearcher.Setup(x => x.Search(It.IsAny<string>())).Returns(Task.FromException<IEnumerable<SearchResultItem>>(new Exception("Kaboom")));
            _searchers.Add(badSearcher.Object);

            var webSearcherService = new WebSearcherService(_querySearcherService.Object);
            var results = await webSearcherService.Search("Test");

            Assert.AreEqual(1, results.ErrorList.Count);
            Assert.AreEqual(1, results.Results.Count());
        }
    }
}
