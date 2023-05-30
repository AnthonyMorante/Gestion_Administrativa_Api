using IdentityServer4.Models;

namespace Gestion_Administrativa_Api.Configuration
{
    public class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
           };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("app.all")
            };


        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("app")
                {
                    Scopes = { "app.all" }
                }
            };

    }
}
