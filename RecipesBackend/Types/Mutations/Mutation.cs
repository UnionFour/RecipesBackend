using MongoDB.Driver;
using RecipesBackend.DAL.Entities;
using RecipesBackend.Services.Auth;

namespace RecipesBackend.Types.Mutations;

// [MutationType]
public class Mutation
{
    public string RegisterUser(
        [Service] IAuthService authService, 
        [Service] IMongoCollection<User> users,
        UserAuth input) => authService.RegisterUser(input, users);

    public string AuthorizeUser(
        [Service] IAuthService authService,
        [Service] IMongoCollection<User> users,
        UserAuth input) => authService.AuthorizeUser(input, users);
}