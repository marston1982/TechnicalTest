using System.Collections.Generic;

namespace Bds.TechTest.Models
{
    public class SearchResultItem
    {
        public SearchResultItem(string title, string link, string source)
        {
            Title = title;
            Link = RemoveScheme(link);
            Source = new List<string> { source };
        }

        private string RemoveScheme(string url)
        {
            return url
                .Replace("http://", "")
                .Replace("https://", "");
        }

        public string Title { get; private set; }
        public string Link { get; private set; }
        public List<string> Source { get; private set; }

        public SearchResultItem Combine(SearchResultItem other)
        {
            foreach (var item in other.Source)
                Source.Add(item);

            if (Title != other.Title)
                Title = $"{Title} / {other.Title}";

            return this;
        }
    }
}
