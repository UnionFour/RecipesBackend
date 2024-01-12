using MongoDB.Bson;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Services.Repositories
{
    public class UserRepository : Repository<int, User, IMongoCollection<User>>
    {
        public UserRepository([Service] IMongoCollection<User> collection) : base(collection) { }
    }
}
