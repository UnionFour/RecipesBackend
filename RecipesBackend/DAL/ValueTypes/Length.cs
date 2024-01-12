using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.ValueTypes
{
    [BsonIgnoreExtraElements]
    public class Length
    {
        public int Number { get; set; }

        [BsonElement("unit")]
        public LocalizedString Unit { get; set; } = new();
    }
}