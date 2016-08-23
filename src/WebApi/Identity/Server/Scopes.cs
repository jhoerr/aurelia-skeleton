using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core.Models;

namespace WebApi.Identity.Server
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
            {
                ////////////////////////
                // identity scopes
                ////////////////////////

                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.Email,
                StandardScopes.Address,
                StandardScopes.OfflineAccess,
                StandardScopes.RolesAlwaysInclude,
                StandardScopes.AllClaims,

                ////////////////////////
                // resource scopes
                ////////////////////////

                new Scope
                {
                    Enabled = true,
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope
                {
                    Enabled = true,
                    Name = "apiAccess",
                    DisplayName = "API Access",
                    Description = "Access to all the APIs",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim(ClaimTypes.Role),
                        new ScopeClaim(ClaimTypes.Name),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.Role),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.Name),
                        new ScopeClaim(IdentityServer3.Core.Constants.ClaimTypes.PreferredUserName),
                    },
                    IncludeAllClaimsForUser = true,
                },
                new Scope
                {
                    Name = "idmgr",
                    DisplayName = "IdentityManager",
                    Type = ScopeType.Resource,
                    Emphasize = true,
                    ShowInDiscoveryDocument = false,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim(ClaimTypes.Role)
                    }
                }
            };

            scopes.AddRange(StandardScopes.All);
            return scopes;
        }
    }
}