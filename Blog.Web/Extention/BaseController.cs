using Blog.Web.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Extention;

public class BaseController:ControllerBase
{
    public int RequestUserId
    {
        get
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
    public string AuthorizationValue
    {
        get
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            return authorizationHeader[7..];
        }
    }

    public override OkObjectResult Ok(object? value = null)
    {
        if (value == null)
        {
            return new OkObjectResult(new ResponseBaseModel());
        }
        return base.Ok(value);
    }
}
