using Serilog;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MovieCollectionAPI
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                // Log the exception using Serilog
                Log.Error(context.Exception, "An unhandled exception occurred.");

                // You can customize the error response based on your requirements
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An unexpected error occurred. Please try again later."),
                    ReasonPhrase = "Internal Server Error"
                };
            }
        }
    }
}