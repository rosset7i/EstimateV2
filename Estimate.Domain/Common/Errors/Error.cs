using System.Net;

namespace Estimate.Domain.Common.Errors;

public class Error
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public Error(
        string message,
        HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}