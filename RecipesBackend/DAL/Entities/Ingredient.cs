using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Ingredient
	{
		// General Info
		public int Id { get; set; }

		[BsonElement("nameClean")]
		public LocalizedString LocalizedName { get; set; } = new();

		[BsonIgnore]
		public string Name => LocalizedName.Rus;

		// to get image "https://spoonacular.com/cdn/ingredients_100x100/" + image
		public string Image { get; set; } = "";

		public double? Amount { get; set; } = null;

		[BsonElement("unit")]
		public LocalizedString? LocalizedUnit { get; set; } = null;

		[BsonIgnore]
		public string? Unit => LocalizedUnit?.Rus;

		[BsonElement("measures")]
		public object? MeasureDocument { get; set; } = null;

		[BsonIgnore]
		public Measure? Measure => BsonSerializer.Deserialize<Measure?>(BsonDocument.Create(MeasureDocument)["metric"].AsBsonDocument);
	}
}