using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Auth
{
    public interface IAuthService
    {
        public string RegisterUser(UserAuth input);

        public string AuthorizeUser(UserAuth input);
    }
}