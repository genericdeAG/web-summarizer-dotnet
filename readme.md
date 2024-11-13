# web-summarizer-dotnet (AI refactored)

This is a test project for refactoring applications to another programming language using AI. The original project was developed in python.

The tool scrapes a single website or landing pages for the most important information and feeds this data into a prompt to OpenAI's GPT-4 API.

It will return short paragraphs summarizing and describing the brand/company, their products/services, and other information from the website.

## Requirements

- .NET 8.0 or higher is required.
- You need to have Chrome WebDriver installed.
- You need to have an OpenAI API key.

## Get Started

To get started, copy the `.env.template` and rename it to `.env`. Add your OpenAI credentials and custom config here (language, default file path).

Then, restore the packages with the following command:

```
dotnet restore
```

If you want to scrape local files, add the path to the file in the `.env` file and place it in the data folder.

## Run Summarizer with CLI Commands

### Available CLI Arguments

- `--url` or `-u`: Specify the URL of the website to analyze (default: from `.env`).
- `--language` or `-l`: Specify the output language (default: from `.env` or English).
- `--focus` or `-f`: Focus the summary on a specific topic (e.g., company, product, technology) - (default: general summary).
- `--length` or `-len`: Control summary length.
  - `small`: One-sentence summary.
  - `medium`: One detailed paragraph (default).
  - `long`: Detailed one-page summary.
  - `keypoints`: Bullet-point summary.

### Examples

```
dotnet run -- --url https://www.generic.de
```
(uses specified URL and default language from `.env`)

```
dotnet run -- --url https://www.generic.de --language Schwäbisch
```
(uses specified URL and specified language)

```
dotnet run -- --url https://www.generic.de --focus product
```
(focuses summary on product-related information)

```
dotnet run -- --url https://www.generic.de --length keypoints
```
(returns summary as bullet points)

```
dotnet run -- --url https://www.generic.de/digitale-produktentwicklung --language Deutsch --focus services --length long
```
(focuses summary on services and returns a detailed one-page summary in German)


**Contact me if you have ideas for improvements or feature requests.**

LinkedIn: https://www.linkedin.com/in/joshua-heller-1b5326140/