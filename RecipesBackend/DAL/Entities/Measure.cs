using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Measure
	{
		public double Amount { get; set; }

		[BsonElement("unitShort")]
		public LocalizedString LocalizedUnitShort { get; set; } = new();

		[BsonIgnore]
		public string UnitShort => LocalizedUnitShort.Rus;

		[BsonElement("unitLong")]
		public LocalizedString LocalizedUnitLong { get; set; } = new();

		[BsonIgnore]
		public string UnitLong => LocalizedUnitLong.Rus;
	}
}