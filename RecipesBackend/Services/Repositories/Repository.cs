using MongoDB.Bson;
using MongoDB.Driver;

namespace RecipesBackend.Services.Repositories
{
    public abstract class Repository<TID ,TModel, TCollection>
        where TCollection : IMongoCollection<TModel>
    {
        private readonly IMongoCollection<TModel> _collection;

        protected Repository(TCollection collection) 
        {
            _collection = collection;
        }

        public TModel GetModel(TID id)
        {
            return _collection.FindAsync(new BsonDocument { { "_id", $"{id}" } }).Result.First();
        }

        public void CreateModel(TModel newInstance)
        {
            _collection.InsertOneAsync(newInstance);
        }

        public void UpdateModel(TID id, TModel updateInstance)
        {
            var filter = new BsonDocument { { "_id", $"{id}" } };
            _collection.UpdateOne(filter, updateInstance.ToBsonDocument());
        }

        public void DeleteModel(TID id)
        {
            throw new NotImplementedException();
        }
    }
}
