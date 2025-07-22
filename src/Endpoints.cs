
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StocksApi.Common.Filters;
using StocksApi.Features.Stocks;

namespace StocksApi;

[ExcludeFromCodeCoverage]
public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("v1")
            .AddEndpointFilter<RequestLoggingFilter>()
            .WithOpenApi();

        endpoints.MapStocksEndpoints();

    }
    private static void MapStocksEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("/stocks")
            .WithTags("Stocks");

        endpoints.MapPublicGroup()
            .MapEndpoint<GetStocks>();
    }
    internal static RouteGroupBuilder MapPublicGroup(this IEndpointRouteBuilder app, string? prefix = null)
    {
        return app.MapGroup(prefix ?? string.Empty)
            .AllowAnonymous();
    }
    internal static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpointMapper
    {
        TEndpoint.Map(app);
        return app;
    }
    internal interface IEndpointMapper
    {
        static abstract void Map(IEndpointRouteBuilder app);
    }
}