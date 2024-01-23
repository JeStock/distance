# Code example

## Getting started
1. Run `docker-compose up` from the root directory.
2. Go to `http://localhost:5788/swagger`

## Thesis (problem statement)
> Simple things should be simple. Complex things should be possible
> -- _Alan Kay_

Here is a problem to solve - validate (or better say _parse_) an input data, collect _all_ the occurred errors and return either the list of errors or successfully parsed data: `rowData -> parser -> ParsedData XOR Error[]`.  
It happened so that almost all mainstream OO languages doesn't have a _simple_ and _reliable_ tool for that simple problem.

In C# this issue typically solved by one of the following 3 approaches:
1. Utilizing exceptions. There are tons and tons written and said why this is not a good solution, just name a few here:
   * Collect all the errors in one go is complicated;
   * Exceptions cannot be used in a type modeling, they cannot be explicitly encoded in a method’s signature;
   * Exceptions are not type-safe. There is no compile-time validation for code correctness;
   * Exception are _designed_ and _named_ to denote something _exceptional_, corrupted data or incerrect user input - is not something exceptional, it's _expected_. 
2. Mixing up responsibilities, e.g. `parsing` and `error reporting` are done in one place, e.g. logger is injected into
domain code to be able to report error immediately where it's discovered. That violates SRP and makes code less testable.
3. Hand-making `Result` types, usually incorrect, like this:
   ```csharp
   public class Result {
       public ParsedData Success { get; set; }
       public List<string> Errors { get; set; }
       public bool IsSuccess => Errors.Count == 0;
   }
   ```
All of them unsafe/incorrect/inaccurate.

## Solution structure
### Places service
Service responsible for 2 things
1. Parse the CSV file with airports' data and index all successfully parsed airports the Elasticsearch index.
2. Provide an API to get an info regarding different places of Earth. For simplicity, it's currently limiting by airports, but could be extended by any places which have (or associated to) a static geolocation: cities, ports, etc.

### Distance service
Distance service accepts 2 airports IATA codes, queries airport info from Places service, and calculates the distance between these airports.

## Services structure
Both services follow the so-called Clean Architecture (also known as Hexagonal/Onion/Ports and adapters).
### Core (Domain) Layer
The domain code is isolated into a separate project and have no dependencies on other projects. It contains the problem domain models, domain-specific logic and public contracts to interact with the domain.  
Each domain model implements [Smart constructor](#type-driven-design) and [Parse, don't validate](#type-driven-design) patterns, which are complementary, both they are contributing to the DDD's approach of encoding the domain's logic into the application's type system,  [making invalid state unrepresentable](#type-driven-design).  

The Core intentionally made having no dependencies on other projects (except 'Shared'), and it's pure: no mutations, no exceptions, no side effects, no DI is used. As a consequence, it's 100% testable without any compromises: no mocks, stubs or fakes needed, tests are simple and straightforward, yet effective.
### Application layer
Here it's a bit artificial, but in a real-world code it'll be the main gateway to the domain of the service, specifying the use cases of the application.
### Api (Presentation) layer
It's responsible for 'RestApi' endpoints, 'OpenApi' specification, HTTP request/response-handling logic, application configuration, and it's a composition root of the whole service.
### Infrastructure layer
It's responsible for all the external dependencies of a service. E.g. `Distance` service have 2 external dependencies: `Places` service and `Redis` cache, so both `Places RestApi Client` and `Redis Repository` are implemented in scope of infrastructure.
### Shared
It contains domain-agnostic code that could be useful anywhere. In real-world application, it'll probably be a NuGet package(s).

## Testing
Unit tests are covering Core projects, no mocks, stubs and fakes are used, all tests are testing the very domain code. [Property-based testing](#property-based-testing) technique is applied.  
In addition, a few integration tests are added (using [WebApplicationFactory](#integration-testing))

## Tech stack
* ASP.NET Core (.NET 8.0)
* Elasticsearch
* Redis
* Serilog
* [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions)
* Swagger
* xUnit
* WebApplicationFactory
* NSubstitute
* [AutoFixture](https://github.com/AutoFixture/AutoFixture)
* [FsCheck](https://fscheck.github.io/FsCheck)
* FluentAssertions

## Links
### Type-driven design
* [Parse, don’t validate pattern](https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate) by Alexis King
* [Smart constructor pattern](https://wiki.haskell.org/Smart_constructors)
* [Functional Programming in C#: 5.4.3 The smart constructor pattern](https://www.manning.com/books/functional-programming-in-c-sharp-second-edition) by Enrico Buonanno
* [Designing with types: Making illegal states unrepresentable](https://fsharpforfunandprofit.com/posts/designing-with-types-making-illegal-states-unrepresentable) by Scott Wlaschin

### Boolean blindness
This repository is using `OperationResult` enum, this is one of the options available to avoid so-called 'Boolean Blindness' antipattern,
when a developer uses a boolean type to represent an application state, which is led to the statements like this:  
```(csharp)
var (isValid, errors) = ValidateSmth(inputModel);
if (!isValid) { ... }
```
The meaning of the returned value is shifted from the value itself to the name of a variable to which the value is assigned,
and the author of the method is out of control over which meaning will be given to the method’s execution result.
Give a different name and you’ll get a different meaning.
In more complicated scenarios, that could and usually do lead to mistakes and bugs.  
For mode details see
* [Boolean blindness](https://existentialtype.wordpress.com/2011/03/15/boolean-blindness/)
* [Boolean Blindness: Don't represent state with boolean!](https://yveskalume.dev/boolean-blindness-dont-represent-state-with-boolean)
* [Code smell: Boolean blindness](https://runtimeverification.com/blog/code-smell-boolean-blindness)

### Property based testing
* [FsCheck](https://fscheck.github.io/FsCheck)
* [Property-Based Testing](https://fsharpforfunandprofit.com/series/property-based-testing)
* [Property-Based Testing with C#](https://www.codit.eu/blog/property-based-testing-with-c)

### Integration testing
* [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)
