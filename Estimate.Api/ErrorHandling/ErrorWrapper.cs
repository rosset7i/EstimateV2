using Estimate.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace Estimate.Api.ErrorHandling;

public class ErrorWrapper : ObjectResultExecutor
{
    public ErrorWrapper(
        OutputFormatterSelector formatterSelector,
        IHttpResponseStreamWriterFactory writerFactory,
        ILoggerFactory loggerFactory,
        IOptions<MvcOptions> mvcOptions)
        : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
    {

    }

    public override Task ExecuteAsync(ActionContext context, ObjectResult result)
    {
        var resultOfType = result.Value?.GetType();

        if (resultOfType is not null
            && resultOfType.IsGenericType
            && resultOfType.GetGenericTypeDefinition() == typeof(ResultOf<>))
        {
            var isError = (bool)(resultOfType.GetProperty("IsError")
                ?.GetValue(result.Value) ?? false);

            if (!isError)
                return base.ExecuteAsync(context, result);

            var error = (Error)resultOfType.GetProperty("FirstError")?
                .GetValue(result.Value)!;

            result.StatusCode = (int)error.StatusCode;
        }

        return base.ExecuteAsync(context, result);
    }
}