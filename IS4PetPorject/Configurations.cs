namespace IdentityServer
{
    using System.Collections.Generic;
    using IdentityModel;
    using IdentityServer4;
    using IdentityServer4.Models;

    /// <summary>
    /// Provides configuration settings for IdentityServer, defining clients, identity resources, API scopes, and API resources.
    /// </summary>
    public class Configurations
    {
        /// <summary>
        /// Configures the identity resources available, such as OpenID and profile information.
        /// </summary>
        /// <returns>A list of identity resources.</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        /// <summary>
        /// Defines the API scopes that clients can request, representing the level of access for each API.
        /// </summary>
        /// <returns>A list of API scopes.</returns>
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("students.read"),
                new ApiScope("courses.read"),
            };

        /// <summary>
        /// Configures API resources available to clients, each linked to specific scopes for granular access control.
        /// </summary>
        /// <returns>A list of API resources.</returns>
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("students")
                {
                    Scopes = { "students.read" },
                },
                new ApiResource("courses")
                {
                    Scopes = { "courses.read" },
                },
            };
    }
}
