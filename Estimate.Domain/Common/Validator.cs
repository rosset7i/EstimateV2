using Estimate.Domain.Common.Errors;

namespace Estimate.Domain.Common;

public class Validator
{
    private readonly List<Error> _errors;

    private Validator()
    {
        _errors = new List<Error>();
    }

    public static Validator New()
    {
        return new Validator();
    }

    public Validator When(
        bool hasError,
        Error error,
        bool onlyIf = true)
    {
        if (hasError && onlyIf)
            _errors.Add(error);

        return this;
    }

    public List<Error> ReturnErrors()
    {
        return _errors;
    }
}