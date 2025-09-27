using System;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public class UnauthorizedException : ApiExceptionBase
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public UnauthorizedException(Exception exception) : base(exception)
    {
    }

    public override int StatusCode => 401;
}