using HtmlAgilityPack;
using System.Net.Http;

namespace WebSummarizer.Services.Scraping
{
    public class HtmlFetcher : IHtmlFetcher
    {
        private readonly HttpClient _httpClient;

        public HtmlFetcher()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HtmlDocument?> FetchHtmlAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(content);
                    return htmlDocument;
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve the webpage. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error fetching the webpage: {e.Message}");
                return null;
            }
        }
    }
}