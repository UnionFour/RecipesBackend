using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Auth
{
    public interface IAuthService
    {
        public UserPayload RegisterUser(UserAuth input);

        public UserPayload AuthorizeUser(UserAuth input);
    }
}