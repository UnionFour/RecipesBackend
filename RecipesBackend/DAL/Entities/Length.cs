using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Length
	{
		public int Number { get; set; }

		[BsonElement("unit")]
		public LocalizedString LocalizedUnit { get; set; } = new();

		[BsonIgnore]
		public string Unit => LocalizedUnit.Rus;
	}
}