namespace GithubToc
{
    internal static class StringExt
    {
        internal static string Slice(this string text, int start, int end)
        {
            return text.Substring(start, end - start);
        }

        internal static string EscapeNumber(this string text)
        {
            var escaped = text.Replace(".", "").Trim();
            if (escaped == string.Empty)
            {
                return string.Empty;
            }

            return escaped + "-";
        }

        internal static string EscapeUri(this string text)
        {
            return text.Trim()
                       .ToLower()
                       .Replace(".", "")
                       .Replace(" ", "-")
                       .Replace(":", "")
                       .TrimEnd('-');
        }
    }
}