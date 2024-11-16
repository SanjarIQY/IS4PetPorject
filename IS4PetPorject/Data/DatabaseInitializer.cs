namespace IdentityServer.Data
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>();

            var user = new IdentityUser()
            {
                UserName = "Toshmat"
            };

            var result = userManager.CreateAsync(user, "1234qwert").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).GetAwaiter().GetResult();
            }
        }
    }
}
