using Serilog;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;

namespace MovieCollectionAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            string logFilePath = @"C:\Logs\log.txt";
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            // Register your Unity container
            var container = new UnityContainer();
            container.RegisterType<IMovieCollectionService, MovieCollectionService>();
            container.RegisterType<IUserService, UserService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}
