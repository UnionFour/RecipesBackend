using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string NickName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}
