using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities;

public class IngredientCollection
{
    [BsonId]
	public string? Id { get; set; }

	public int Count { get; set; } = 1;
}