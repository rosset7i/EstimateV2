namespace Estimate.Domain.Common.Errors;

public record struct ResultOf<TValue>
{
    private readonly TValue? _value = default;
    private TValue Value => _value!;
    private Error[] Errors { get; set; }
    // private Error? FirstError => Errors.Any() ? Errors[0] : null;

    private ResultOf(List<Error> errors)
    {
        Errors = errors.ToArray();
    }

    private ResultOf(Error error)
    {
        Errors = new[] {error};
    }

    private ResultOf(TValue value)
    {
        _value = value;
    }

    public static implicit operator ResultOf<TValue>(TValue value)
    {
        return new ResultOf<TValue>(value);
    }

    public static implicit operator ResultOf<TValue>(Error value)
    {
        return new ResultOf<TValue>(value);
    }

    public static implicit operator ResultOf<TValue>(List<Error> value)
    {
        return new ResultOf<TValue>(value);
    }
}