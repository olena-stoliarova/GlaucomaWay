using GlaucomaWay.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlaucomaWay.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiController(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

            AuthenticatedUser = _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result;
        }

        public User AuthenticatedUser { get; }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool HasPermission(User user)
            => IsAdmin() || AuthenticatedUser.Id == user.Id;

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool IsAdmin()
        {
            var roles = _userManager.GetRolesAsync(AuthenticatedUser).Result;
            return roles.Contains(Role.Admin);
        }
    }
}
