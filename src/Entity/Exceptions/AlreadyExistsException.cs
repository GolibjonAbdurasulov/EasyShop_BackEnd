using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class AlreadyExistsException : ApiExceptionBase
{
    public AlreadyExistsException(string message) : base(message)
    {
        this.StatusCode = 403;
    }
}