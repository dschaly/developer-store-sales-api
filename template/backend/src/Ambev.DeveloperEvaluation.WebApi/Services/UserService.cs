using Ambev.DeveloperEvaluation.Common.Security;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Services;

public class UserService : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Id => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    public string Username => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    public string Role => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
}