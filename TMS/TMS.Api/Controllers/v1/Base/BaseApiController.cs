using Microsoft.AspNetCore.Mvc;

namespace TMS.Api.Controllers.v1.Base;

[Produces("application/json")]
[Route("[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    public static string CurrentUser { get; protected set; }

    public BaseApiController()
    {
        CurrentUser = GetUser(HttpContext);
    }

    protected string GetUser(HttpContext context)
    {
        if (context == null) return "";
        var user = context.User.Identities.Where(w => w.Claims.Any(a => a.Type.Equals("id"))).FirstOrDefault();
        return "user1";
    }

}
