namespace GithubToc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class TableOfContents
    {
        public static string Create(string markdown)
        {
            var sb = new StringBuilder();
            foreach (var header in GetHeaders(markdown).Select(HeaderRow.Parse))
            {
                sb.AppendLine(header.ToString());
            }

            return sb.ToString();
        }

        public static IEnumerable<string> GetHeaders(string markdown)
        {
            return markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                           .Where(HeaderRow.IsMarkdownHeaderRow);
        }
    }
}
