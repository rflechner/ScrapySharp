namespace ScrapySharp.IntegrationTests.ScrapingServicesSamples
{
    public static class FluentParsing
    {
        public static (bool successfullyParsed, decimal value) TryParseDecimal(string text) => (decimal.TryParse(text, out var v), v);
        
        public static (bool successfullyParsed, int value) TryParseInt(string text) => (int.TryParse(text, out var v), v);
    }
}