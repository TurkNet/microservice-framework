using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Noctools.Application.Middlewares
{
    /// <summary>
    /// https://www.netsparker.com.tr/blog/web-guvenligi/referrer-policy-security-header/
    /// https://www.netsparker.com.tr/blog/web-guvenligi/csp-content-security-policy/
    /// https://www.netsparker.com.tr/blog/web-guvenligi/http-guvenlik-headerlari/
    /// https://www.netsparker.com.tr/blog/web-guvenligi/hosgeldin-feature-policy-ve-yolcudur-x-xss-protection/
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
            httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            httpContext.Response.Headers.Add("Referrer-Policy", "no-referrer");
            httpContext.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none';" +
                                                               " geolocation 'none'; gyroscope 'none'; " +
                                                               "magnetometer 'none'; microphone 'none'; " +
                                                               "payment 'none'; usb 'none'");

            await next(httpContext);
        }
    }
}
