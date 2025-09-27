using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class BadRequestException : ApiExceptionBase
{
    public BadRequestException(string message) : base(message)
    {
        this.StatusCode = 400;
    }
}