using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.ValueTypes
{
    [BsonIgnoreExtraElements]
    public class Instruction
    {
        [BsonElement("step")]
        public LocalizedString Name { get; set; } = new();

        public List<Step> Steps { get; set; } = new();
    }
}
