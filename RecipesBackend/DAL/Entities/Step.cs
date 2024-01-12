using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Step
	{
		public int? Number { get; set; }

		public List<Ingredient>? Ingredients { get; set; } = new();

		[BsonElement("step")]
		public LocalizedString Description { get; set; } = new();

		public List<Equipment>? Equipments { get; set; } = new();

		public Length? Length { get; set; } = null;
	}
}