namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsentInputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Button { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> ScopesConsented { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public bool RememberConsent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReturnUrl { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
