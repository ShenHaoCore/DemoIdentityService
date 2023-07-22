namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ScopeViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool Emphasize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Checked { get; set; }
    }
}
