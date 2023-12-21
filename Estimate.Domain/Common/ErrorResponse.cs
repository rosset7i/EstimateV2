namespace Estimate.Domain.Common;

public class ErrorResponse
{
    public int Status { get; set; }
    public string[] Errors { get; set; }

    public ErrorResponse(
        int status,
        string[] errors)
    {
        Status = status;
        Errors = errors;
    }
}