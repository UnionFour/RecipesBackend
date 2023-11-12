using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
    public class Instruction
    {
	    [BsonElement("step")]
        public LocalizedString LocalizedName { get; set; } = new();

        [BsonIgnore]
        public string Name => LocalizedName.Rus;
        
        public List<Step> Steps { get; set; } = new();
    }
}
