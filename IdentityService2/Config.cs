using IdentityServer4.Models;
using IdentityServer4;

namespace IdentityService2
{
    /// <summary>
    /// 
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("Implicit_scope1")
        };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
            new ApiResource("api1", "api1") {
                Scopes = { "Implicit_scope1" },
                ApiSecrets = { new Secret("apipwd".Sha256()) } // API 密钥
            }
        };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Client> Clients => new Client[] {
            new Client {
                ClientId = "Implicit_client",
                ClientName = "Implicit Auth",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "http://localhost:5002/signin-oidc", }, // 跳转登录到的客户端的地址
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc", }, // 跳转登出到的客户端的地址
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "Implicit_scope1" },
                RequireConsent = true // 是否需要同意授权（默认是FALSE）
            },
        };
    }
}
