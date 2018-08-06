namespace GithubToc
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class StringExt
    {
        internal static string Slice(this string text, int start, int end)
        {
            return text.Substring(start, end - start);
        }

        internal static string EscapeNumber(this string text)
        {
            var escaped = text.Replace(".", string.Empty).Trim();
            if (escaped == string.Empty)
            {
                return string.Empty;
            }

            return escaped + "-";
        }

        internal static string EscapeUri(this string text)
        {
            return new string(EscapedUriCore(text).ToArray()).Trim('-');
        }

        private static IEnumerable<char> EscapedUriCore(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c == '.' || c == '(' || c == ')')
                {
                    continue;
                }

                if (c == '&')
                {
                    int length;
                    if (TryEscapeHtmlEntity(text, i, out length))
                    {
                        i += length;
                        continue;
                    }
                }

                if (char.IsLetterOrDigit(c))
                {
                    yield return char.ToLower(c);
                    continue;
                }

                if (i > 0 && i < text.Length - 1)
                {
                    if (i > 2)
                    {
                        if (!char.IsLetterOrDigit(text[i - 1]) && !char.IsLetterOrDigit(text[i - 2]))
                        {
                            continue;
                        }
                    }

                    yield return '-';
                }
            }
        }

        private static bool TryEscapeHtmlEntity(string text, int start, out int length)
        {
            if (text[start] != '&')
            {
                length = 0;
                return false;
            }

            for (int i = start; i < text.Length; i++)
            {
                var c = text[i];
                if (char.IsWhiteSpace(c))
                {
                    length = 0;
                    return false;
                }

                if (c == ';')
                {
                    length = i - start;
                    return true;
                }
            }

            length = 0;
            return false;
        }
    }
}