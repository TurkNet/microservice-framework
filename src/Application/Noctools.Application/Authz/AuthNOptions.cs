using System.Collections.Generic;

namespace Noctools.Application.Authz
{
    public class AuthNOptions
    {
        public Dictionary<string, string> ClaimToScopeMap { get; set; }
        public Dictionary<string, string> Scopes { get; set; }
        public string Audience { get; set; }
    }
}
