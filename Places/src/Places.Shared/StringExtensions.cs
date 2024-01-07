using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Places.Shared;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str) => 
        string.IsNullOrWhiteSpace(str);

    public static bool DontMatchPattern(this string str, string pattern) =>
        Regex.IsMatch(str, pattern);
}