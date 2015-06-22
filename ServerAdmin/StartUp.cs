using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ServerAdmin.Provider;

[assembly: OwinStartup(typeof(ServerAdmin.Startup))]
namespace ServerAdmin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // app.UseCors(CorsOptions.AllowAll);

            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new InternalAuthorisationProvider(),
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}