using IdentityServer4.Models;

namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessConsentResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsRedirect => RedirectUri != null;

        /// <summary>
        /// 
        /// </summary>
        public string RedirectUri { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public Client Client { get; set; } = new Client();

        /// <summary>
        /// 
        /// </summary>
        public bool ShowView => ViewModel != null;

        /// <summary>
        /// 
        /// </summary>
        public ConsentViewModel? ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasValidationError => ValidationError != null;

        /// <summary>
        /// 
        /// </summary>
        public string ValidationError { get; set; } = string.Empty;
    }
}
