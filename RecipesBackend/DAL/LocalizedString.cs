using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL;

[BsonIgnoreExtraElements]
public class LocalizedString
{
	public string Rus { get; set; } = "";
	public string Eng { get; set; } = "";
}