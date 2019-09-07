namespace Bds.TechTest.Models
{
    public class SearchResultItem
    {
        public SearchResultItem(string title, string link, string source)
        {
            Title = title;
            Link = link;
            Source = source;
        }

        public string Title { get; private set; }
        public string Link { get; private set; }
        public string Source { get; private set; }
    }
}
