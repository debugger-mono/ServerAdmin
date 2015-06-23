using System;
using System.Web.Http;
using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ServerAdmin.App_Start;
using ServerAdmin.Provider;

[assembly: OwinStartup(typeof(ServerAdmin.Startup))]
namespace ServerAdmin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // app.UseCors(CorsOptions.AllowAll);

            IContainer container = AutofacConfig.SetUp();
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(GlobalConfiguration.Configuration);


            ConfigureOAuth(app, container);
        }

        public void ConfigureOAuth(IAppBuilder app, IContainer container)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = container.Resolve<InternalAuthorisationProvider>(),
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}