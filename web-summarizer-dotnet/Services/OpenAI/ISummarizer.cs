using WebSummarizer.Models;

namespace WebSummarizer.Services.OpenAI
{
    public interface ISummarizer
    {
        Task<string> SummarizeAsync(
            WebsiteContent content, 
            string language, 
            string? summaryFocus = null, 
            string summaryLength = "medium"
        );
    }
}