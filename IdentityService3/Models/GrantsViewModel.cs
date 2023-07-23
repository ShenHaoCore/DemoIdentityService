namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GrantsViewModel
    {
        public IEnumerable<GrantViewModel> Grants { get; set; } = new List<GrantViewModel>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class GrantViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

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
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> IdentityGrantNames { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> ApiGrantNames { get; set; } = new List<string>();
    }
}
