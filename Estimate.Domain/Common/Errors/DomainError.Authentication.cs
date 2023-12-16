using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Domain.Common.Errors;

public static partial class DomainError
{
    public static class Authentication
    {
        public static Error WrongEmailOrPassword => new("Wrong email or password.", HttpStatusCode.BadRequest);
        public static Error EmailAlreadyInUse => new("Email already in use.", HttpStatusCode.Conflict);
        public static Error RegisterError(IEnumerable<IdentityError> errors) => new(string.Join(' ', errors.Select(e => e.Description)), HttpStatusCode.Conflict);
    }
}