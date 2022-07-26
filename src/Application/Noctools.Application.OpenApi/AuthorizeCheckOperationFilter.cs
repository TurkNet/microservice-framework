using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Noctools.Application.OpenApi
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            // Check for authorize attribute
            context.ApiDescription.TryGetMethodInfo(out var methodInfo);
            var requiredScopes = methodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>();

            /*var hasAuthorize = context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().Any() ||
                               context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().Any();*/

            var hasAuthorize = requiredScopes.Any();
            if (!hasAuthorize) return;

            operation.Responses.Add("401", new Response {Description = "Unauthorized"});
            operation.Responses.Add("403", new Response {Description = "Forbidden"});

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>>
                {
                    ["oauth2"] = new[] {"swagger_id"}
                }
            };
        }
    }
}
