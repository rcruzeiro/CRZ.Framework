using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CRZ.Framework.Utils
{
    public sealed class RegexParser
    {
        readonly Regex _pattern;

        public RegexParser(Regex pattern)
        {
            _pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
        }

        public bool TryParse(string input, out string output)
        {
            output = null;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = _pattern.Match(input);

                if (match != null)
                {
                    if (!match.Success) return false;

                    StringBuilder result = new StringBuilder();

                    for (int i = 1; i < match.Groups?.Count; i++)
                    {
                        string value = match.Groups[i].Value;

                        if (!string.IsNullOrWhiteSpace(value))
                            result.Append(value);
                    }

                    output = result.ToString();

                    return match.Success;
                }
            }

            return false;
        }

        public bool TryParse(string input, out string[] segments)
        {
            segments = null;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var match = _pattern.Match(input);

                if (match != null && match.Groups != null)
                {
                    if (!match.Success) return false;

                    var result = new List<string>();

                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        string value = match.Groups[i].Value;

                        if (!string.IsNullOrWhiteSpace(value))
                            result.Add(value);
                    }

                    segments = result.ToArray();

                    return match.Success;
                }
            }

            return false;
        }

        public override string ToString() => $"Pattern: {_pattern.ToString()}";
    }
}
