using System.Net;
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
        result.StatusCode = (int)HttpStatusCode.Conflict;
        return base.ExecuteAsync(context, result);
    }
}