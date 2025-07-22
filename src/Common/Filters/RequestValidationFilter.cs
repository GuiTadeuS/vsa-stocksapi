using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StocksApi.Common.Filters;

public class RequestValidationFilter<TRequest>(ILogger<RequestValidationFilter<TRequest>> logger, IValidator<TRequest>? validator = null) : IEndpointFilter
{
    public record ValidationError(int StatusCode, IDictionary<string, string[]> Errors, int ErrorsCount);
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestName = typeof(TRequest).FullName;

        if (validator is null)
        {
            // logger.LogInformation("{Request}: No validator configured.", requestName);
            return await next(context);
        }

        // logger.LogInformation("{Request}: Validating...", requestName);
        var request = context.Arguments.OfType<TRequest>().First();
        var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            var validationError = new ValidationError(400, errors, errors.Count);
            return TypedResults.BadRequest(validationError);
        }

        // logger.LogInformation("{Request}: Validation succeeded.", requestName);
        return await next(context);
    }
}