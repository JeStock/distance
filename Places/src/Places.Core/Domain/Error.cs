namespace Places.Core.Domain;

public class Error
{
    private List<string> Errors { get; } = [];

    public Error(string errorMessage) => Errors.Add(errorMessage);
    public Error(IEnumerable<string> errors) => Errors.AddRange(errors);

    public override string ToString() => $"[ {string.Join(", ", Errors)} ]";
}