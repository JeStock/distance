FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /app-source
COPY src/ src/
COPY tests/ tests/
COPY *.sln .
RUN dotnet restore
RUN dotnet test tests/Places.Core.Tests/Places.Core.Tests.csproj 
RUN dotnet publish -c release -o /app-publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app-publish
COPY --from=builder ./app-publish /app-publish
CMD ["sh", "-c", "dotnet Places.Api.dll"]
