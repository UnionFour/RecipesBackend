using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Repositories
{
    public class RecipeRepository : Repository<string, Recipe, IMongoCollection<Recipe>>
    {
        public RecipeRepository( [Service] IMongoCollection<Recipe> collection) : base(collection) { }
    }
}
