using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
        new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalog_fullpermission","full access for catalog API"),
            new ApiScope("photo_stock_fullpermission","full access for photo stock API"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };
// client cre akış tipinde refresh token olmaz sabit clientID , client script var herszman token alırsın
// username pass varsa refresh token lazım

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client{ ClientId="WebMvcClient",
            ClientName="Asp.Net.Core MVC",
            ClientSecrets={new Secret("secret".Sha256()) },//veritabanından da çekebilirsin
            AllowedGrantTypes=GrantTypes.ClientCredentials,
            AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
            }
        };
}
