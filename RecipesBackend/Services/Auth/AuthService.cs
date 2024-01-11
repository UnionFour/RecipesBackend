using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private AuthOptions AuthOptions { get; }
        private ITimeLimitedDataProtector TimeLimitedDataProtector { get; }

        public AuthService(IOptions<AuthOptions> authOptions,
            IDataProtectionProvider dataProtectionProvider)
        {
            AuthOptions = authOptions.Value;
            TimeLimitedDataProtector = dataProtectionProvider
                .CreateProtector("auth")
                .ToTimeLimitedDataProtector();
        }

        public string RegisterUser(UserAuth input, [Service] IMongoCollection<User> users)
        {
            var filter =  new BsonDocument { { "email", $"{input.Email}" }, { "passHash", $"{input.Password}" } };
            var user = users.Find(filter).FirstOrDefault();
            if (user == null)
                users.InsertOne(new User { Email = input.Email, HashPassword = input.Email});
            else
                throw new Exception(message: "user with such Email is already exists");

            return AuthorizeUser(new UserAuth() { Email = input.Email, Password = input.Password }, users);
        }

        public string AuthorizeUser(UserAuth input, [Service] IMongoCollection<User> users)
        {
            var filter = new BsonDocument { { "email", $"{input.Email}" }, { "passHash", true } };
            var user = users.Find(filter).FirstOrDefault();

            if (user == null) throw new Exception(message: "User is not registrated");
            if (user.HashPassword != input.Password) throw new Exception(message: "wrong Password or Email");

            var handler = new JsonWebTokenHandler();
            var accessToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
                {
                    [JwtRegisteredClaimNames.Email] = input.Email,
                    [JwtRegisteredClaimNames.CHash] = input.Password,
                    [JwtRegisteredClaimNames.Sub] = user?.Id ?? throw new InvalidDataException()
                },
                Issuer = AuthOptions.Issuer,
                Audience = AuthOptions.Audience,
                Expires = DateTime.Now.AddMinutes(15),
                TokenType = "Bearer",
                SigningCredentials = new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            });

            return accessToken;
        }
    }
}
