using System.Text;

namespace Auth
{
    public class AuthOptions
    {
        public const string Issuer = "https://localhost:7207";

        public const string Audience = "https://localhost:7207";

        const string Key = "this is super super secret key!!!";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(Key));
    }
}