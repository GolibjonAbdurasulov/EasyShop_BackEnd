using System.Net;
using System.Text.Json.Serialization;

namespace API.Common;

public class ResponseModelBase : ResponseModelBase<object>
{
    public ResponseModelBase(Exception? exception, HttpStatusCode code = HttpStatusCode.InternalServerError) : base(
        exception, code)
    {
    }

    public ResponseModelBase(object? content, HttpStatusCode code = HttpStatusCode.OK) : base(content, code)
    {
    }

    public ResponseModelBase(HttpStatusCode code) : base(code)
    {
    }

 
    public ResponseModelBase(IEnumerable<object> content, int total, HttpStatusCode code = HttpStatusCode.OK) :
        base(code)
    {
        Content = content;
        Code = code;
        Total = total;
    }

    public ResponseModelBase(string message, HttpStatusCode code = HttpStatusCode.OK) : base(message, code)
    {
        Content = message;
        Code = code;
    }

    public static implicit operator ResponseModelBase(string s)
    {
        return new ResponseModelBase(s);
    }

    public static ResponseModelBase FromIConvertible(IConvertible s)
    {
        return new ResponseModelBase(s);
    }

    public static implicit operator ResponseModelBase((string content, int statusCode) data)
    {
        return new ResponseModelBase(data.content, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase((IConvertible content, int statusCode) data)
    {
        return new ResponseModelBase(data.content, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase((IEnumerable<object> items, int total) data)
    {
        return new ResponseModelBase(data.items, data.total);
    }

    public static implicit operator ResponseModelBase((IEnumerable<IComparable> items, int total) data)
    {
        return new ResponseModelBase(data.items, data.total);
    }

    public static implicit operator ResponseModelBase((IEnumerable<object> items, int total, int statusCode) data)
    {
        return new ResponseModelBase(data.items, data.total, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator
        ResponseModelBase((IEnumerable<IComparable> items, int total, int statusCode) data)
    {
        return new ResponseModelBase(data.items, data.total, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase(Exception exception)
    {
        return new ResponseModelBase(exception);
    }

    public static implicit operator ResponseModelBase((Exception exception, int statusCode) data)
    {
        return new ResponseModelBase(data.exception, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase((object? content, int statusCode) data)
    {
        return new ResponseModelBase(data.content, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase(int statusCode)
    {
        return new ResponseModelBase((HttpStatusCode)statusCode);
    }
}

public class ResponseModelBase<T>
{
    [JsonInclude] public Guid Id = Guid.NewGuid();

    public ResponseModelBase(Exception? exception, HttpStatusCode code = HttpStatusCode.InternalServerError)
    {
        if (exception is not null)
        {
            
                Error = exception?.Message + (exception.InnerException?.Message != null
                    ? ". Inner Message: " + exception.InnerException.Message
                    : "");
        }

        Code = code;
        StackTrace = exception?.StackTrace;
    }

    public ResponseModelBase(T? content, HttpStatusCode code = HttpStatusCode.OK)
    {
        Content = content;
        Code = code;
    }


    public ResponseModelBase(HttpStatusCode code)
    {
        Code = code;
    }

    


    public HttpStatusCode Code { get; init; } = HttpStatusCode.OK;
    public T? Content { get; init; }
    public string? Error { get; init; }
    public int? Total { get; set; }
    public List<ModelErrorState>? ModelStateError { get; init; }
    [JsonIgnore] public string? StackTrace { get; init; }


    public static ResponseModelBase<T> ResultFromException(Exception exception,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ResponseModelBase<T>(exception, statusCode);
    }

  


    public static implicit operator ResponseModelBase<T>(Exception exception)
    {
        return new ResponseModelBase<T>(exception);
    }

    public static implicit operator ResponseModelBase<T>((Exception exception, int statusCode) data)
    {
        return new ResponseModelBase<T>(data.exception, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase<T>((T? content, int statusCode) data)
    {
        return new ResponseModelBase<T>(data.content, (HttpStatusCode)data.statusCode);
    }

    public static implicit operator ResponseModelBase<T>(int statusCode)
    {
        return new ResponseModelBase<T>((HttpStatusCode)statusCode);
    }
}

public class ModelErrorState
{
    public string Key { get; set; }
    public string ErrorMessage { get; set; }
}