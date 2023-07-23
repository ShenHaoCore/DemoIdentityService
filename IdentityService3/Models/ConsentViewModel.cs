namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsentViewModel : ConsentInputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ClientUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string ClientLogoUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; } = new List<ScopeViewModel>();

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ScopeViewModel> ApiScopes { get; set; } = new List<ScopeViewModel>();
    }
}
