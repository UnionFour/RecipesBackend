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
        private IMongoCollection<User> Users;

        public AuthService(IOptions<AuthOptions> authOptions,
            IDataProtectionProvider dataProtectionProvider,
            [Service] IMongoCollection<User> users)
        {
            AuthOptions = authOptions.Value;
            Users = users;
            TimeLimitedDataProtector = dataProtectionProvider
                .CreateProtector("auth")
                .ToTimeLimitedDataProtector();
        }

        // TODO: хеширование пароля
        public UserPayload RegisterUser(UserAuth input)
        {
            var filter = new BsonDocument { { "email", $"{input.Email}" } };
            var user = Users.Find(filter).FirstOrDefault();
            if (user == null)
                Users.InsertOne(new User { Email = input.Email, passHash = input.Password});
            else
                throw new Exception(message: "user with such Email is already exists");

            return AuthorizeUser(new UserAuth { Email = input.Email, Password = input.Password });
        }

        public UserPayload AuthorizeUser(UserAuth input)
        {
            var filter = new BsonDocument { { "email", $"{input.Email}" } };
            var user = Users.Find(filter).FirstOrDefault();

            if (user == null) throw new Exception(message: "User is not registrated");
            if (user.passHash != input.Password) throw new Exception(message: "wrong Password or Email");

            var handler = new JsonWebTokenHandler();
            var accessToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>
                {
                    [JwtRegisteredClaimNames.Email] = input.Email,
                    [JwtRegisteredClaimNames.Sub] = user.Id ?? throw new InvalidDataException()
                },
                Issuer = AuthOptions.Issuer,
                Audience = AuthOptions.Audience,
                Expires = DateTime.Now.AddMinutes(15),
                TokenType = "Bearer",
                SigningCredentials = new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            });

            return new UserPayload
            {
                Id = user.Id,
                Login = user.Email,
                Token = accessToken
            };
        }
    }
}
