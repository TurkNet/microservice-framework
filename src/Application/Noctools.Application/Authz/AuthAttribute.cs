using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Noctools.Application.Authz
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string policy = null)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Policy = policy;
        }
    }
}
