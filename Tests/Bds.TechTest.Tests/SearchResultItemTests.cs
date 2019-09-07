using Bds.TechTest.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bds.TechTest.Tests
{
    public class SearchResultItemTests
    {
        [Test]
        public void When_creating_search_results_item_the_urls_scheme_is_removed_when_its_https()
        {
            var expectedUrl = "test.com";
            var item = new SearchResultItem("test", $"https://{expectedUrl}", "Google");
            Assert.AreEqual(expectedUrl, item.Link);
        }

        [Test]
        public void When_creating_search_results_item_the_urls_scheme_is_removed_when_its_http()
        {
            var expectedUrl = "test.com";
            var item = new SearchResultItem("test", $"http://{expectedUrl}", "Google");
            Assert.AreEqual(expectedUrl, item.Link);
        }
        [Test]
        public void When_creating_search_results_item_if_the_url_has_no_scheme_included_the_link_remains_unchanged()
        {
            var expectedUrl = "test.com";
            var item = new SearchResultItem("test", expectedUrl, "Google");
            Assert.AreEqual(expectedUrl, item.Link);
        }

        [Test]
        public void When_combining_result_items_their_sources_are_combined()
        {
            var first = new SearchResultItem("test", "www.blah.com", "Google");
            var second = new SearchResultItem("test", "www.blah.com", "Bing");
            var combined = first.Combine(second);

            Assert.AreEqual(2, combined.Source.Count);
        }

        [Test]
        public void When_combining_result_items_that_have_different_titles_have_their_titles_concatenated()
        {
            var first = new SearchResultItem("first", "www.blah.com", "Google");
            var second = new SearchResultItem("second", "www.blah.com", "Bing");
            var combined = first.Combine(second);

            Assert.AreEqual("first / second", combined.Title);
        }
    }
}
