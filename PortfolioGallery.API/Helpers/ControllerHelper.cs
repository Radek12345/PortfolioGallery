using System.Security.Claims;

namespace PortfolioGallery.API.Helpers
{
    public static class ControllerHelper
    {
        public static bool IsAllowedUser(int userId, ClaimsPrincipal user)
        {
            return userId == int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}