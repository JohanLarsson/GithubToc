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
            var escaped = text.Replace(".", "").Trim();
            if (escaped == string.Empty)
            {
                return string.Empty;
            }

            return escaped + "-";
        }

        internal static string EscapeUri(this string text)
        {
            return new string(EscapedUriCore(text).ToArray());
        }

        private static IEnumerable<char> EscapedUriCore(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c == '.')
                {
                    continue;
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
    }
}