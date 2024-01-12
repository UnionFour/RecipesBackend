using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Repositories
{
    public class IngredientsRepository : Repository<int, Ingredient, IMongoCollection<Ingredient>>
    {
        public IngredientsRepository(IMongoCollection<Ingredient> collection) : base(collection) { }
    }
}
