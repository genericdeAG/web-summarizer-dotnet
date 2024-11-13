using DotNetEnv;

namespace WebSummarizer.Configuration
{
    public static class AppSettings
    {
        static AppSettings()
        {
            // Load environment variables from .env file
            Env.Load();
        }

        public static string DefaultFilePath
        {
            get => Environment.GetEnvironmentVariable("DEFAULT_FILE_PATH") ?? string.Empty;
        }

        public static string DefaultLanguage
        {
            get => Environment.GetEnvironmentVariable("LANGUAGE") ?? "english";
        }

        public static string OpenAIApiKey
        {
            get
            {
                var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set");
                }
                return apiKey;
            }
        }

        public static string OpenAIModel
        {
            get => Environment.GetEnvironmentVariable("OPENAI_MODEL") ?? "gpt-4o";
        }
    }
}
