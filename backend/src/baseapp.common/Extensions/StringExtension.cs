namespace BaseApp.Common.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string? text)
        {
            return !string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text);
        }
    }
}