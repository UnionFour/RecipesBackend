using MongoDB.Bson.Serialization.Attributes;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.DAL.ValueTypes
{
    [BsonIgnoreExtraElements]
    public class Instruction
    {
        public LocalizedString? Name { get; set; } = new();
        
        public List<Step> Steps { get; set; } = new();
    }
}
