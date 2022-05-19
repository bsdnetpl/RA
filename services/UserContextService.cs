using System.Security.Claims;

namespace RA.services
{
    public interface IUserContextService
    {
        int? GetVGetUserId { get; }
        ClaimsPrincipal User { get; }
    }
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal User => _HttpContextAccessor.HttpContext?.User;
        public int? GetVGetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
