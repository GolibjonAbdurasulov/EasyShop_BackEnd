using System;

namespace Entity.Exceptions.Common;

public class ApiExceptionBase : Exception
{
    public ApiExceptionBase(string message) : base(message)
    {
    }

    public ApiExceptionBase(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ApiExceptionBase(Exception exception) : base(exception.Message, exception)
    {
        StatusCode = 500;
    }

    public virtual int StatusCode { get; set; }
}