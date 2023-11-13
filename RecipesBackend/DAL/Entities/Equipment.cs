using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Equipment
	{
		public int Id { get; set; }

		[BsonElement("name")]
		public LocalizedString Name { get; set; } = new();

		public string Image { get; set; } = "";
	}
}