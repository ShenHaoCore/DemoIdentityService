namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ConfirmUserCode { get; set; }
    }
}
