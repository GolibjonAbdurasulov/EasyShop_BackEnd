using System;
using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public class ExpiredException : ApiExceptionBase
{
    public ExpiredException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}