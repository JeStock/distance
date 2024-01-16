using System.Text.RegularExpressions;

namespace Distance.Api.Configuration;

internal partial class KebabCaseTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value) =>
        value?.ToString() switch
        {
            { } path => RegexPattern().Replace(path, "$1-$2").ToLower(),
            _ => null!
        };

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex RegexPattern();
}