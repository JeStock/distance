using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Places.Api.Configuration;

public static class RoutesConventionsConfiguration
{
    public static void AddRoutesConventions(this MvcOptions options) =>
        options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseTransformer()));
}