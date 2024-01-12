using MongoDB.Driver;
using RecipesBackend.DAL.Entities;
using RecipesBackend.Services.Auth;

namespace RecipesBackend.Types.Mutations;

// [MutationType]
public class Mutation
{
    public string RegisterUser(
        [Service] IAuthService authService,
        UserAuth input) => authService.RegisterUser(input);

    public string AuthorizeUser(
        [Service] IAuthService authService,
        UserAuth input) => authService.AuthorizeUser(input);
}