using GlaucomaWay.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlaucomaWay.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public ApiController(UserManager<User> userManager)
        {
            _userManager = userManager;
            AuthenticatedUser = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
        }

        public User AuthenticatedUser { get; }

        [ApiExplorerSettings(IgnoreApi = true)]
        public bool HasPermission(User user) => AuthenticatedUser.Id == user.Id;
    }
}
