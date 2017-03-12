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
        private User _user;
        private Conversation _conversation;

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
                    _user = user;
                    _conversation = dbContext.Conversation.FirstOrDefault(a => a.UserId == user.Id && a.FinishDate == null);

                    var role = user.IsDoctor ? "Doctor" : "Patient";
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));

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

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            if (!_user.IsDoctor)
            {
                System.Guid? conversationId = null;
                if (_conversation != null)
                    conversationId = _conversation.Id;
                context.AdditionalResponseParameters.Add("conversationId", conversationId); 
            }
            return base.TokenEndpoint(context);
        }
    }
}