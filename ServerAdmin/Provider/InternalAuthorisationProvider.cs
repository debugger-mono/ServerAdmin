using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using ServerAdmin.DataAccess;
using ServerAdmin.DataAccess.Models;

namespace ServerAdmin.Provider
{
    public class InternalAuthorisationProvider : OAuthAuthorizationServerProvider
    {
        private readonly DataAccessHandler dataAccess;
        private readonly UserAccountHandler userAccountHandler;

        public InternalAuthorisationProvider()
        {
            this.dataAccess = new DataAccessHandler();
            this.userAccountHandler = new UserAccountHandler(this.dataAccess);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            bool success = this.userAccountHandler.Validate(new UserAccount { Username = context.UserName, Password = context.Password });

            if (success)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("username", context.UserName));
                identity.AddClaim(new Claim("role", "system administrator"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid grant", "The username or password is incorrect");
                context.Rejected();
            }

            return base.GrantResourceOwnerCredentials(context);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                context.Rejected();
            }

            // TODO: Validate client here
            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                context.OwinContext.Set<string>("oauth:client", "WebClient-ServerAdmin");
                context.Validated();
            }

            return base.ValidateClientAuthentication(context);
        }
    }
}