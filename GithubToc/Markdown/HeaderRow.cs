namespace GithubToc
{
    public class HeaderRow
    {
        private readonly string indentation;
        private readonly string linkText;
        private readonly string uriText;

        public HeaderRow(string indentation, string linkText, string uriText)
        {
            this.indentation = indentation;
            this.linkText = linkText;
            this.uriText = uriText;
        }

        public static HeaderRow Parse(string row)
        {
            var indentation = ParseIndentation(row, out var pos);
            var number = ParseNumber(row, ref pos);
            var text = row.Substring(pos);
            var linkText = number + text;
            var uri = $"#{number.EscapeNumber()}{text.EscapeUri()}";
            return new HeaderRow(indentation, linkText.Trim(), uri);
        }

        public static bool IsMarkdownHeaderRow(string row)
        {
            return row.StartsWith("#");
        }

        public override string ToString()
        {
            return $"{this.indentation}- [{this.linkText}]({this.uriText})";
        }

        private static string ParseIndentation(string row, out int pos)
        {
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] != '#')
                {
                    if (i == 0)
                    {
                        pos = 0;
                        return string.Empty;
                    }

                    pos = i;
                    return new string(' ', (i - 1) * 2);
                }
            }

            pos = 0;
            return string.Empty;
        }

        private static string ParseNumber(string row, ref int pos)
        {
            while (pos < row.Length && char.IsWhiteSpace(row[pos]))
            {
                pos++;
            }

            var startAt = pos;
            while (pos < row.Length && (char.IsDigit(row, pos) || row[pos] == '.'))
            {
                pos++;
            }

            return row.Slice(startAt, pos);
        }
    }
}