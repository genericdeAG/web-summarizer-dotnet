using HtmlAgilityPack;

namespace WebSummarizer.Services.Scraping
{
    public interface IHtmlFetcher
    {
        Task<HtmlDocument?> FetchHtmlAsync(string url);
    }
}