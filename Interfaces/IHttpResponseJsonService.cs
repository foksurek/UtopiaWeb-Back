namespace UtopiaWeb.Interfaces;

public interface IHttpResponseJsonService
{
    public object Ok(object data);
    public object BadRequest(List<string> details = null!);
    public object NotFound(string message = "Not found");
    public object Unauthorized(List<string> details = null!);
}