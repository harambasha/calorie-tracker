using CT.WebAPI.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using CT.Logging;
using CT.Logging.Models;
using CT.Repository.Infrastructure;
using CT.Repository.Infrastructure.Identity;
using CT.WebAPI.HttpPipeline;
using Microsoft.Owin.Cors;


namespace CT.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            // Configure Log4net logger
            var log4NetSettings = new FileInfo(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase.Replace("bin", "Config"), "Log4Net.xml"));
            log4net.Config.XmlConfigurator.Configure(log4NetSettings);

            // Configure HTTP Logging feature
            app.UseHttpLogging(new HttpLoggingOptions
            {
                TrackingStore = new HttpLoggingStore(),
                TrackingIdPropertyName = "x-tracking-id",
                MaximumRecordedRequestLength = 64 * 1024,
                MaximumRecordedResponseLength = 64 * 1024
            });

            ConfigureWebAPI(httpConfig);

            app.UseWebApi(httpConfig);

        }

        internal void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider()
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }


        internal void ConfigureWebAPI(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            // Handle all exceptions the same way by returning JSON data (serialized ErrorMessage object)
            config.Filters.Add(new ApiExceptionFilterAttribute());
            config.MessageHandlers.Add(new CrossDomainHandler());
            config.EnsureInitialized();
        }
    }
}
