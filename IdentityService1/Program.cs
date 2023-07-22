using IdentityService1;

var builder = WebApplication.CreateBuilder(args);
var identity = builder.Services.AddIdentityServer().AddTestUsers(TestUsers.Users);
identity.AddInMemoryIdentityResources(Config.IdentityResources);
identity.AddInMemoryApiScopes(Config.ApiScopes);
identity.AddInMemoryApiResources(Config.ApiResources);
identity.AddInMemoryClients(Config.Clients);
identity.AddDeveloperSigningCredential();

var app = builder.Build();
app.UseIdentityServer();
app.MapGet("/", () => "Hello World!");

app.Run();
