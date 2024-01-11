using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Auth
{
    public class AuthOptions
    {
        public string Issuer { get; set; } = "";

        public string Audience { get; set; } = "";

        public string Key { get; set; } = "";

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(Key));
    }
}