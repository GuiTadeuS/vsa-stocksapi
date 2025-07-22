using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using StocksApi.Common.Extensions;
using StocksApi.Common.Helpers;
using StocksApi.Common.Results;
using StocksApi.Data;
using StocksApi.Models;
using static StocksApi.Endpoints;

namespace StocksApi.Features.Stocks;

public class GetStocks : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("/", Handle)
        .WithSummary("Gets all posts")
        .WithRequestValidation<Request>();

    public record Request(int? Page, int? PageSize) : IPagedRequest;
    public class RequestValidator : PagedRequestValidator<Request>;

    private static async Task<Results<Ok<Response<PagedList<Stock>>>, InternalServerError<Response<string>>>> Handle(
        [AsParameters] Request request, ApplicationDbContext context)
    {
        try
        {
            var data = await context.Stocks.ToPagedListAsync(request, s => s.OrderBy(s => s.Id));
            
            return TypedResults.Ok(Response.SuccessResponse(200, data, data.Items.Count));
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError(Response.ErrorResponse<string>(500, ex.Message));
        }
    }
}

