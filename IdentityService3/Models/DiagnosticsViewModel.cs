using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace IdentityService3.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DiagnosticsViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            AuthenticateResult = result;

            if (result.Properties!.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticateResult AuthenticateResult { get; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> Clients { get; } = new List<string>();
    }
}
