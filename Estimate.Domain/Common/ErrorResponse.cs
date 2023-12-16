using Newtonsoft.Json;

namespace Estimate.Domain.Common;

public class ErrorResponse
{
    public string Title { get; set; }
    public string TraceId { get; set; }
    public int Status { get; set; }
    public string[] Errors { get; set; }

    public ErrorResponse(
        string title,
        string traceId,
        int status,
        string[] errors)
    {
        Title = title;
        TraceId = traceId;
        Status = status;
        Errors = errors;
    }

    public override string ToString() =>
        JsonConvert.SerializeObject(this);
}