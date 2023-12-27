using System.Text.Json.Serialization;

namespace Estimate.Domain.Common.Errors;

public class ResultOf<TValue>  
{
    public TValue Result { get; private set; }
    public List<Error> Errors { get; } = new();

    [JsonIgnore]
    public bool IsError => Errors.Any();

    [JsonIgnore]
    public Error FirstError => (Errors.Any() ? Errors[0] : null)!;

    private ResultOf(TValue result)
    {
        Result = result;
    }

    private ResultOf(Error error)
    {
        Errors.Add(error);
    }

    private ResultOf(List<Error> errors)
    {
        Errors.AddRange(errors);
    }

    public static implicit operator ResultOf<TValue>(TValue value)
    {
        return new ResultOf<TValue>(value);
    }

    public static implicit operator ResultOf<TValue>(Error error)
    {
        return new ResultOf<TValue>(error);
    }

    public static implicit operator ResultOf<TValue>(List<Error> errors)
    {
        return new ResultOf<TValue>(errors);
    }
}