using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities;

[BsonIgnoreExtraElements]
public class Nutrition
{
	public CaloricBreakdown CaloricBreakdown { get; set; }
}