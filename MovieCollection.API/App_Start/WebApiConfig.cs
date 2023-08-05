using System.Web.Http;

namespace MovieCollectionAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new AuthorizeAttribute());          

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Custom route for the Search method
            config.Routes.MapHttpRoute(
                name: "SearchCollection",
                routeTemplate: "api/movie/search",
                defaults: new { controller = "Movie", action = "Search" }
            );

            // Register the custom exception filter
            config.Filters.Add(new CustomExceptionFilterAttribute());
        }
    }
}
