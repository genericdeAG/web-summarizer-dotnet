namespace WebSummarizer.Models
{
    public class WebsiteContent
    {
        public string? Url { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public List<string> Headings { get; set; }
        public string? Footer { get; set; }
        public List<string> Texts { get; set; }
        public List<string> Images { get; set; }

        public WebsiteContent()
        {
            Headings = new List<string>();
            Texts = new List<string>();
            Images = new List<string>();
        }

        public WebsiteContent(
            string url,
            string language,
            string title,
            List<string> headings,
            string footer,
            List<string> texts,
            List<string> images)
        {
            Url = url;
            Language = language;
            Title = title;
            Headings = headings ?? new List<string>();
            Footer = footer;
            Texts = texts ?? new List<string>();
            Images = images ?? new List<string>();
        }

        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "url", Url ?? string.Empty },
                { "language", Language ?? string.Empty },
                { "title", Title ?? string.Empty },
                { "headings", Headings },
                { "footer", Footer ?? string.Empty },
                { "texts", Texts },
                { "images", Images }
            };
        }
    }
}