using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities;

public class IngredientCollection : Entity<string>
{
    public IngredientCollection(string id) : base(id) { }

    [BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public new string? Id { get; set; }
	public int Count { get; set; }
}