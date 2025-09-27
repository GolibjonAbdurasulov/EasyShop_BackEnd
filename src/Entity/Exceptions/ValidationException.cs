using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class ValidationException : ApiExceptionBase
{
    public ValidationException(string message) : base(message)
    {
        this.StatusCode = 400;
    }
}