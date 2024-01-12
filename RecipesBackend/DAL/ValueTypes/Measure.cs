using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.ValueTypes
{
    [BsonIgnoreExtraElements]
    public class Measure
    {
        public double Amount { get; set; }

        [BsonElement("unitShort")]
        public LocalizedString UnitShort { get; set; } = new();

        [BsonElement("unitLong")]
        public LocalizedString UnitLong { get; set; } = new();
    }
}