using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.ValueTypes;

[BsonIgnoreExtraElements]
public class Nutrition
{
    public CaloricBreakdown CaloricBreakdown { get; set; }
}