namespace IdentityServer.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authentication;

    /// <summary>
    /// Represents the data model for user login, including required fields for username, password, and a return URL.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username for the login request.
        /// This field is required.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for the login request.
        /// This field is required.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the URL to redirect to after a successful login.
        /// This field is required.
        /// </summary>
        [Required]
        public string ReturnUrl { get; set; }
    }
}
