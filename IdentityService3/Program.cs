using IdentityService3;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var identity = builder.Services.AddIdentityServer().AddTestUsers(TestUsers.Users);
identity.AddInMemoryIdentityResources(Config.IdentityResources);
identity.AddInMemoryApiScopes(Config.ApiScopes);
identity.AddInMemoryApiResources(Config.ApiResources);
identity.AddInMemoryClients(Config.Clients);
identity.AddDeveloperSigningCredential();

var app = builder.Build();
app.UseStaticFiles();
app.UseIdentityServer();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
