using UtopiaWeb.Interfaces;

namespace UtopiaWeb.Services;

public class HttpResponseJsonService : IHttpResponseJsonService
{
    public object Ok(object data)
    {
        return new { code = 200, data };
    }

    public object BadRequest(List<string> details)
    {
        details ??= [];
        return new { Code = 400, Message = "Bad request", Details = details };
    }

    public object NotFound(string message = "Not found")
    {
        return new { Code = 404, Message = message };
    }

    public object Unauthorized(List<string> details = null!)
    {
        details ??= [];
        return new { Code = 401, Message = "Unauthorized", Details = details };
    }
}