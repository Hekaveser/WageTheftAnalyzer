using WageTheftAnalyzer.Features.Inflation;
using WageTheftAnalyzer.Features.User;
using WageTheftAnalyzer.Features.Wage;

namespace WageTheftAnalyzer.Features;

public static class Features
{
    public static IServiceCollection AddFeatures(this WebApplicationBuilder builder)
    {
        return builder.Services
            .AddWagesFeature()
            .AddUsersFeature()
            .AddInflationsFeature();
    }

    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/");

        group.MapWagesFeature();
        group.MapUsersFeature();
        group.MapInflationsFeature();

        return endpoints;
    }
}
