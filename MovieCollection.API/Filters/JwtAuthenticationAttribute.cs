using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MovieCollectionAPI
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

            else
                context.Principal = principal;
        }

        private static bool ValidateToken(string token, out User user)
        {
            user = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            var roleClaim = identity.FindFirst(ClaimTypes.Role);

            if (userIdClaim == null || usernameClaim == null || roleClaim == null)
                return false;

            if (!Enum.TryParse(roleClaim.Value, out Roles userRole))
                return false;
            
            user = new User
            {
                Id = int.Parse(userIdClaim.Value),
                Username = usernameClaim.Value,
                Role = userRole
                // Add other properties as needed
            };

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out User user))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                   new Claim(ClaimTypes.Name, user.Username), // Add the username claim
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal principal = new ClaimsPrincipal(identity);

                return Task.FromResult(principal);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}