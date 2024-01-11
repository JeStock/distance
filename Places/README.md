# Places service

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
4. LanguageExt

## TODO
1. Add domain layer which will be responsible to build a search query for airport by the following fields: Name, IataCode, IcaoCode
2. Add an endpoint which will accept the query as plain text
3. Add unit tests
4. (Optional) add integration tests
5. (Optional) add health checks + ELK
6. (Optional) Use Elasticsearch Geospatial data for Lat/Lon.


## Links
### Type-driven design
1. [Parse, don’t validate pattern](https://lexi-lambda.github.io/blog/2019/11/05/parse-don-t-validate) by Alexis King
2. [Smart constructor pattern](https://wiki.haskell.org/Smart_constructors)
3. [Functional Programming in C#: 5.4.3 The smart constructor pattern](https://www.manning.com/books/functional-programming-in-c-sharp-second-edition) by Enrico Buonanno
4. [Designing with types: Making illegal states unrepresentable](https://fsharpforfunandprofit.com/posts/designing-with-types-making-illegal-states-unrepresentable) by Scott Wlaschin