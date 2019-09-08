using Bds.TechTest.Models;
using Bds.TechTest.Models.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;

namespace Bds.TechTest.Tests
{
    public class HomePageViewModelTests
    {
        [Test]
        public void HasSearchResults_returns_true_when_there_are_results()
        {
            var model = new HomePageViewModel();
            model.SearchResults = new List<SearchResultItem> { new SearchResultItem("a", "b", "c") };
            Assert.IsTrue(model.HasSearchResults);
        }
        [Test]
        public void HasSearchResults_returns_false_when_there_are_no_results()
        {
            Assert.IsFalse(new HomePageViewModel().HasSearchResults);
        }

        [Test]
        public void HasErrors_returns_true_when_there_are_errors()
        {
            var model = new HomePageViewModel();
            model.Errors = new List<string> { "test" };
            Assert.IsTrue(model.HasErrors);
        }
        [Test]
        public void HasErrors_returns_false_when_there_are_no_errors()
        {
            Assert.IsFalse(new HomePageViewModel().HasErrors);
        }
    }
}
