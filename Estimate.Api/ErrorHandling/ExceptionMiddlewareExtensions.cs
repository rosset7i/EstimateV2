using Estimate.Domain.Common;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Estimate.Api.ErrorHandling;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerProvider loggerProvider)
    {
        var logger = loggerProvider.CreateLogger(nameof(ExceptionMiddlewareExtensions));
        
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                var exception = (BusinessException)contextFeature?.Error!;
                
                if (contextFeature is not null)
                {
                    context.Response.StatusCode = (int)exception.FirstError.StatusCode;
                    context.Response.ContentType = "application/json";
                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    var exceptionErrors = exception.Errors.Select(e => e.Message).ToArray();

                    var formattedJson = JsonConvert.SerializeObject(
                        new ErrorResponse(
                                exception.Message,
                                context.TraceIdentifier,
                                (int)exception.FirstError.StatusCode,
                                exceptionErrors),
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    await context.Response.WriteAsync(formattedJson);
                }
            });
        });
    }
}