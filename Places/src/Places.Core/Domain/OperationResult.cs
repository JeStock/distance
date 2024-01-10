namespace Places.Core.Domain;

/// <summary>
/// Represents the result of an operation on elastic index.
/// </summary>
/// <remarks>
/// This is one of the options available to avoid so-called 'Boolean Blindness' antipattern,
/// when a developer uses a boolean type to represent an application state, which is led to the statements like this: <br/>
/// <code> var isDeleted = repository.Delete(id); </code>
/// Here, the user of repository's API decided that 'true' means 'deleted', not the designer of the API.
/// The boolean itself doesn't mean anything, it doesn't carry any business information on.
/// In more complicated scenarios, that could and usually do lead to mistakes and bugs. <br/>
/// <br/> For mode details see <br/>
/// * <a href="https://existentialtype.wordpress.com/2011/03/15/boolean-blindness/">Boolean blindness</a><br/>
/// * <a href="https://yveskalume.dev/boolean-blindness-dont-represent-state-with-boolean">Boolean Blindness: Don't represent state with boolean!</a><br/>
/// * <a href="https://runtimeverification.com/blog/code-smell-boolean-blindness">Code smell: Boolean blindness</a>
/// </remarks>
public enum OperationResult
{
    Success = 1,
    Failure = 2
}