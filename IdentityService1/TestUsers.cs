using IdentityModel;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityService1
{
    /// <summary>
    /// 
    /// </summary>
    public class TestUsers
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser> {
                    new TestUser {
                        SubjectId = "1",
                        Username = "Shenhao",
                        Password = "123456",
                        Claims = {
                            new Claim(JwtClaimTypes.Name, "Shenhao Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Shenhao"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "Shenhao@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://Shenhao.top"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}
