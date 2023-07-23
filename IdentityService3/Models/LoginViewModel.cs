namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : LoginInputModel
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AllowRememberLogin { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool EnableLocalLogin { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        /// <summary>
        /// 
        /// </summary>
        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;

        /// <summary>
        /// 
        /// </summary>
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
