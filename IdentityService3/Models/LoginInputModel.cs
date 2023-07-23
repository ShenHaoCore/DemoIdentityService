using System.ComponentModel.DataAnnotations;

namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
