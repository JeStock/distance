# Places service
Service responsible for 2 things
1. Parse the CSV file with airports' data and index all successfully parsed into the Elasticsearch index.
2. Provide an API to get an info regarding different places of Earth. For simplicity, it's currently limiting by airports, 
but could be extended by any places which have (or associated to) a static geolocation: cities, ports, etc.

## The Problem
> Simple things should be simple. Complex things should be possible
> -- _Alan Kay_

Here is a problem to solve:
Validate (or better say parse) an input data, collect _all_ the occurred errors and return either
the list of errors (if any) or successfully parsed data:  
`rowData -> parser -> ParseData XOR Error[]`.
It happened so that almost all mainstream OO languages doesn't have a _simple_ tool for the following simple problem.
  
This issue typically solved in C# in one of the following 3 approaches. All of them unsafe/incorrect/inaccurate:
1. Utilizing exceptions. There are tons and tons written and said why this is not a good solution, just name a few here:
   * Collect all the errors is complicated. Throwing an exception terminates the execution flow immediately. It is possible to aggregate them manually, though it's not really convenient, nonetheless the main issues remain:
   * Exceptions cannot be used in a type modeling, they cannot be explicitly encoded in a method’s signature, which naturally leads to appearing of "exceptional" flows.
   * There is no compile-time validation for code correctness. Even if the method does nothing but throw exceptions, the compiler will keep silent.
2. Mixing up responsibilities, e.g. `parsing` and `error reporting` are done in one place - error logged immediately where it's discovered.
3. Hand-made `Result` types, usually done incorrect, like this:
   ```csharp
   public class Result {
       public ParsedData Success { get; set; }
       public List<string> Errors { get; set; }
       public bool IsSuccess => Errors.Count == 0;
   }
   ```




## Api endpoints
TODO: Enlist endpoints
See `/swagger` to get more details.

## Service structure
## Api (Presentation) layer
## Infrastructure layer
## Core (Domain) Layer
This is the heart of the Clean/Hexagonal/Onion/Ports-n-adapters architecture.
The Core (Domain) layer contains the problem domain models and the domain-specific logic.  
Each domain model implements [Smart constructor](#type-driven-design) and [Parse, don't validate](#type-driven-design) patterns,
which are complementary, both contributing to the DDD's approach of encoding the domain's logic into the application's type system, 
[making invalid state unrepresentable](#type-driven-design).  

The Core intentionally made having no dependencies, and it's pure: no mutations, no exceptions, no side effects, no DI is used.
As a consequence, it's 100% testable without any compromises: no mocks, stubs or fakes needed, tests are simple and straightforward, yet effective.

## Tech stack
1. ASP.NET Rest Api
2. Elasticsearch
3. Serilog
4. CSharpFunctionalExtensions

## TODO
1. Add domain layer which will be responsible to build a search query for airport by the following fields: Name, IataCode, IcaoCode
   * Add AirportsService to map query models to elastic search query and query result back to api responses
   * Consider Specification Pattern
2. Add an endpoint which will accept the query as plain text
3. Add unit tests
4. (Optional) add integration tests
5. (Optional) add health checks + ELK
6. (Optional) Use Elasticsearch Geospatial data for Lat/Lon.
7. Index all parsing errors for further analysis
8. Add yet another DTO


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
