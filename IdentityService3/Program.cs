using IdentityServer4;
using IdentityService3;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var identity = builder.Services.AddIdentityServer().AddTestUsers(TestUsers.Users);
identity.AddInMemoryIdentityResources(Config.IdentityResources);
identity.AddInMemoryApiScopes(Config.ApiScopes);
identity.AddInMemoryApiResources(Config.ApiResources);
identity.AddInMemoryClients(Config.Clients);
identity.AddDeveloperSigningCredential();

builder.Services.AddAuthentication().AddGoogle("Google", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme; 
    options.ClientId = "test"; 
    options.ClientSecret = "test";
}).AddOpenIdConnect("aad", "Sign-in with Azure AD", options =>
{
    options.Authority = "https://login.microsoftonline.com/common";
    options.ClientId = "https://leastprivilegelabs.onmicrosoft.com/38196330-e766-4051-ad10-14596c7e97d3";

    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

    options.ResponseType = "id_token";
    options.CallbackPath = "/signin-aad";
    options.SignedOutCallbackPath = "/signout-callback-aad";
    options.RemoteSignOutPath = "/signout-aad";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidAudience = "165b99fd-195f-4d93-a111-3e679246e6a9",

        NameClaimType = "name",
        RoleClaimType = "role"
    };
}).AddLocalApi(options =>
{
    options.ExpectedScope = "api";
});

var app = builder.Build();
app.UseStaticFiles();
app.UseIdentityServer();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default", 
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
