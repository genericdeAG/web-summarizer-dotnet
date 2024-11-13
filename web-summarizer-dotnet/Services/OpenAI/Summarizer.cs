using OpenAI;
using OpenAI.Chat;
using WebSummarizer.Configuration;
using WebSummarizer.Models;

namespace WebSummarizer.Services.OpenAI
{
    public class Summarizer : ISummarizer
    {
        private readonly ChatClient _client;

        public Summarizer()
        {
            _client = new ChatClient(
                model: "gpt-4",
                apiKey: AppSettings.OpenAIApiKey
            );
        }

        private string BuildPrompt(WebsiteContent content, string language, string? summaryFocus, string summaryLength)
        {
            var prompt = $"Please analyze this website content and provide a {summaryLength} summary in {language}.\n\n";
            
            if (!string.IsNullOrEmpty(summaryFocus))
            {
                prompt += $"Focus on {summaryFocus}.\n\n";
            }

            prompt += $"URL: {content.Url}\n";
            prompt += $"Title: {content.Title}\n";
            
            if (content.Headings.Any())
            {
                prompt += "Headings:\n";
                foreach (var heading in content.Headings)
                {
                    prompt += $"- {heading}\n";
                }
            }

            if (!string.IsNullOrEmpty(content.Footer))
            {
                prompt += $"\nFooter: {content.Footer}\n";
            }

            if (content.Texts.Any())
            {
                prompt += "\nMain Content:\n";
                foreach (var text in content.Texts)
                {
                    prompt += $"{text}\n";
                }
            }

            return prompt;
        }

        public async Task<string> SummarizeAsync(
            WebsiteContent content,
            string language,
            string? summaryFocus = null,
            string summaryLength = "medium")
        {
            var prompt = BuildPrompt(content, language, summaryFocus, summaryLength);

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful assistant summarizing the content and information of landing pages."),
                new UserChatMessage(prompt)
            };

            var completion = await _client.CompleteChatAsync(messages);
            Console.WriteLine(completion);
            return completion.Value.Content[0].Text;
        }
    }
}