using System.CommandLine;
using OpenAI;
using WebSummarizer.Configuration;
using WebSummarizer.Services.OpenAI;
using WebSummarizer.Services.Scraping;

// Create command-line options
var urlOption = new Option<string>(
    name: "--url",
    description: "URL of the website to analyze",
    getDefaultValue: () => AppSettings.DefaultFilePath);

var languageOption = new Option<string>(
    name: "--language",
    description: "Language for the summary",
    getDefaultValue: () => AppSettings.DefaultLanguage);

var summaryFocusOption = new Option<string?>(
    name: "--focus",
    description: "Focus area for the summary (e.g., 'company', 'product', 'technology')");

var summaryLengthOption = new Option<string>(
    name: "--length",
    description: "Length of the summary",
    getDefaultValue: () => "medium")
{
    IsRequired = false
};

summaryLengthOption.AddAlias("-len");
summaryLengthOption.FromAmong("small", "medium", "long", "keypoints");

// Create root command
var rootCommand = new RootCommand("Web Summarizer - Analyzes websites and provides summaries");
rootCommand.AddOption(urlOption);
rootCommand.AddOption(languageOption);
rootCommand.AddOption(summaryFocusOption);
rootCommand.AddOption(summaryLengthOption);

// Define the command handler
rootCommand.SetHandler(async (url, language, focus, length) =>
{
    try
    {
        // Initialize services
        var htmlFetcher = new HtmlFetcher();
        var openAIClient = new OpenAIClient(AppSettings.OpenAIApiKey);
        var summarizer = new Summarizer();

        var htmlDocument = await htmlFetcher.FetchHtmlAsync(url);
        if (htmlDocument == null)
        {
            Console.WriteLine("Failed to fetch website content.");
            return;
        }

        var websiteContent = HtmlParser.ParseContent(htmlDocument, url);
        var summary = await summarizer.SummarizeAsync(websiteContent, language, focus, length);
        
        Console.WriteLine(summary);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Environment.Exit(1);
    }
}, urlOption, languageOption, summaryFocusOption, summaryLengthOption);

return await rootCommand.InvokeAsync(args);
