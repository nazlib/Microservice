using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
        new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
        new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
        new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
        new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
        new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };
    //clientların kullanıcının hangi bilgilerine erişsein
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),//jwt tokenınn subkeyword,
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource()
            {
                Name="roles",
                DisplayName= "Roles",
                Description="Kullanıcı rolleri",
                UserClaims=new []{ "role"} 
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalog_fullpermission","full access for catalog API"),
            new ApiScope("photo_stock_fullpermission","full access for photo stock API"),
            new ApiScope("basket_fullpermission","full access for basket API"),
            new ApiScope("discount_fullpermission","full access for discount API"),
            new ApiScope("order_fullpermission","full access for order API"),
            new ApiScope("payment_fullpermission","full access for payment API"),
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
            AllowedGrantTypes=GrantTypes.ClientCredentials,//refresh token bunda yok
            AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
            },
            new Client
            {
                ClientName="Asp.Net Core MVC",
                ClientId="WebMvcClientForUser",
                AllowOfflineAccess=true,//offline izin veriyoruz
                ClientSecrets= {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,//refresh token icin tanımlıyoz
                AllowedScopes={ "basket_fullpermission", "discount_fullpermission","order_fullpermission","payment_fullpermission",// "gateway_fullpermission",
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,
                    //bu olmazsa yani elimizde refresh token yoksa tekrar email ve password bilgisi 1 saat sonra tekrar login ekranına gönder userı
                    //çözüm accesstokenın ömrünü uzun yap(önerilmez) yada refreshtokenı al yeni token oluştur
                    IdentityServerConstants.StandardScopes.OfflineAccess,//kullanıcı offline olsa dahi kullanıcı adına refresh token ile yeni bi token alabilirz
                    IdentityServerConstants.LocalApi.ScopeName,//kendi identity serverımıza local istek yapmak icin bu scope a sahip olmalı
                    "roles" },
                AccessTokenLifetime=1*60*60,//1hour
                RefreshTokenExpiration=TokenExpiration.Absolute,//sliding option refresh token istedikçe ömrünü artttırcak
                AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,//refresh token 60 gün sonra expire
                RefreshTokenUsage= TokenUsage.ReUse//
            }
            //kullanıcı her girdiğinde bir refresh token gelecek ama eğer bir kere girdi 61. gün girmek istediğinde tekrar login ekranına gitcek expire oldu

        };
}
