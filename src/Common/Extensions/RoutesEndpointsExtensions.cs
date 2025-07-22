using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StocksApi.Common.Filters;

namespace StocksApi.Common.Extensions;

public static class RoutesEndpointsExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder route)
    {
        return route
            .AddEndpointFilter<RequestValidationFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
