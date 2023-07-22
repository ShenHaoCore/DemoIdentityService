using IdentityServer4.Models;

namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorViewModel()
        {
            this.Error = new ErrorMessage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        /// <summary>
        /// 
        /// </summary>
        public ErrorMessage Error { get; set; }
    }
}
