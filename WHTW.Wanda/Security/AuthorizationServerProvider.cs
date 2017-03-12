using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using WHTW.Wanda.Models;

namespace WHTW.Wanda.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                using (var dbContext = new AppContext())
                {
                    var password = context.Password.Encode();
                    var user = dbContext.User.FirstOrDefault(a => a.RG.ToLower() == context.UserName.ToLower() && a.Password == password);
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "Invalid RG or Password!");
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim(new Claim("id", user.Id.ToString()));

                    var principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;

                    context.Validated(identity);
                }
            }
            catch
            {
                context.SetError("invalid_grant", "Failed to login, please try again!");
            }
        }
    }
}