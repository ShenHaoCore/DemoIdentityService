namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggedOutViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string PostLogoutRedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string SignOutIframeUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LogoutId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;

        /// <summary>
        /// 
        /// </summary>
        public string? ExternalAuthenticationScheme { get; set; }
    }
}
