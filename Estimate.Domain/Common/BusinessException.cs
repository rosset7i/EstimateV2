using Estimate.Domain.Common.Errors;

namespace Estimate.Domain.Common;

[Serializable]
public class BusinessException : Exception
{
    public Error[] Errors { get; set; }
    public Error FirstError => Errors[0];

    public BusinessException(List<Error> errors)
    {
        Errors = errors.ToArray();
    }

    public BusinessException(Error error)
    {
        Errors = new[] {error};
    }
}