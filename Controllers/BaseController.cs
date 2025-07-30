using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgriConnect.Controllers
{
    [ApiController]
    public class BaseController :ControllerBase
    {
        protected int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        protected string UserRole => User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
