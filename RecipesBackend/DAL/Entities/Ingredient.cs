using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Ingredient
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nameClean")]
		public LocalizedString Name { get; set; } = new();

		// to get image "https://spoonacular.com/cdn/ingredients_100x100/" + image
		public string? Image { get; set; } = "";

		public double? Amount { get; set; } = null;

		[BsonElement("unit")]
		public LocalizedString? Unit { get; set; } = null;

		// TODO: сделать меру нормальным классом
		[GraphQLIgnore]
		[BsonElement("measures")]
		public object? MeasureDocument { get; set; } = null;
	}
}