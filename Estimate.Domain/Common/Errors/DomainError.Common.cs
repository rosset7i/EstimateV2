using System.Net;

namespace Estimate.Domain.Common.Errors;

public static partial class DomainError
{
    public static class Common
    {
        public static Error NotFound<T>() => new($"{typeof(T).Name} not found!" , HttpStatusCode.NotFound);
        public static Error InvalidDomain<T>() => new($"Invalid {typeof(T).Name} values, please contact an administrator!" , HttpStatusCode.NotFound);
    }
}