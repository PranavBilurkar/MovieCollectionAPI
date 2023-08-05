using System.Security.Claims;
using System.Security.Principal;

namespace MovieCollectionAPI
{
    public class AuthorizationHelper
    {
        public static bool IsAuthorized(int ownerId, IPrincipal user)
        {
            var userIdentity = user.Identity as ClaimsIdentity;
            if (userIdentity == null || !userIdentity.IsAuthenticated)
            {
                return false;
            }

            var userIdClaim = userIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var isAdmin = user.IsInRole(Roles.Admin.ToString());
            var isOwner = userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId) && userId == ownerId;

            return isAdmin || isOwner;
        }

    }
}