using Microsoft.AspNetCore.Identity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using IS4PetPorject;
using IdentityServer;
using IdentityServer.Data;
using Microsoft.EntityFrameworkCore;
using IdentityModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseInMemoryDatabase("MEMORY");
})
    .AddIdentity<IdentityUser, IdentityRole>(config =>
    {
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
        config.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var clients = builder.Configuration.GetSection("IdentityServer:Clients").Get<List<Client>>();

clients.ForEach(client =>
{
    foreach (var secret in client.ClientSecrets)
    {
        secret.Value = secret.Value.ToSha256();
    }
});

builder.Services.AddIdentityServer(option =>
{
    option.UserInteraction.LoginUrl = "/Auth/Login";
})
    .AddAspNetIdentity<IdentityUser>()
    .AddInMemoryClients(clients)
    .AddInMemoryApiResources(Configurations.GetApiResources())
    .AddInMemoryIdentityResources(Configurations.GetIdentityResources())
    .AddInMemoryApiScopes(Configurations.GetApiScopes())
    .AddDeveloperSigningCredential();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DatabaseInitializer.Init(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
