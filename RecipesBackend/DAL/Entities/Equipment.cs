using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Equipment
	{
		public int Id { get; set; }

		[BsonElement("name")]
		public LocalizedString LocalizedName { get; set; } = new();

		[BsonIgnore]
		public string Name => LocalizedName.Rus;

		public string Image { get; set; } = "";
	}
}