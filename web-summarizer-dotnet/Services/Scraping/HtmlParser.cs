using HtmlAgilityPack;
using WebSummarizer.Models;

namespace WebSummarizer.Services.Scraping
{
    public static class HtmlParser
    {
        public static string GetLanguage(HtmlDocument document)
        {
            var html = document.DocumentNode.SelectSingleNode("//html");
            return html?.GetAttributeValue("lang", "en") ?? "en";
        }

        public static string GetTitle(HtmlDocument document)
        {
            var titleNode = document.DocumentNode.SelectSingleNode("//title");
            return titleNode?.InnerText.Trim() ?? string.Empty;
        }

        public static List<string> GetHeadings(HtmlDocument document)
        {
            var headings = new List<string>();
            for (int i = 1; i <= 6; i++)
            {
                var nodes = document.DocumentNode.SelectNodes($"//h{i}");
                if (nodes != null)
                {
                    headings.AddRange(nodes.Select(n => n.InnerText.Trim()));
                }
            }
            return headings;
        }

        public static string GetFooter(HtmlDocument document)
        {
            var footer = document.DocumentNode.SelectSingleNode("//footer");
            return footer?.InnerText.Trim() ?? string.Empty;
        }

        public static List<string> GetTexts(HtmlDocument document)
        {
            var texts = new List<string>();
            var paragraphs = document.DocumentNode.SelectNodes("//p");
            
            if (paragraphs != null)
            {
                texts.AddRange(paragraphs
                    .Select(p => p.InnerText.Trim())
                    .Where(text => !string.IsNullOrWhiteSpace(text)));
            }
            
            return texts;
        }

        public static List<string> GetImages(HtmlDocument document)
        {
            var images = new List<string>();
            var imageNodes = document.DocumentNode.SelectNodes("//img");
            
            if (imageNodes != null)
            {
                images.AddRange(imageNodes
                    .Select(img => img.GetAttributeValue("src", ""))
                    .Where(src => !string.IsNullOrWhiteSpace(src)));
            }
            
            return images;
        }

        public static WebsiteContent ParseContent(HtmlDocument document, string url)
        {
            return new WebsiteContent(
                url: url,
                language: GetLanguage(document),
                title: GetTitle(document),
                headings: GetHeadings(document),
                footer: GetFooter(document),
                texts: GetTexts(document),
                images: GetImages(document)
            );
        }
    }
}