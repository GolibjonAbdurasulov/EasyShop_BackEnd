using Entity.Exceptions.Common;

namespace Entity.Exceptions;

public sealed class NotFoundException : ApiExceptionBase
{
    public NotFoundException(string message) : base(message)
    {
        this.StatusCode = 404;
    }

    public static void ThrowIfNull(object? data, string message = "Not found")
    {
        if (data is null) throw new NotFoundException(message);
    }
}