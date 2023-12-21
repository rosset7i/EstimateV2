namespace Estimate.Domain.Common;

public class ErrorResponse
{
    public string Title { get; set; }
    public int Status { get; set; }
    public string[] Errors { get; set; }

    public ErrorResponse(
        string title,
        int status,
        string[] errors)
    {
        Title = title;
        Status = status;
        Errors = errors;
    }
}