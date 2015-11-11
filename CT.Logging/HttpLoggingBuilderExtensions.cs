using CT.Logging.Models;
using Owin;

namespace CT.Logging
{
    public static class HttpLoggingBuilderExtensions
    {
        public static IAppBuilder UseHttpLogging(this IAppBuilder builder, HttpLoggingOptions options)
        {
            return builder.Use<HttpLogging>(options);
        }
    }
}
