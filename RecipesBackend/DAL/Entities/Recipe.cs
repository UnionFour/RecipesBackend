using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RecipesBackend.DAL.ValueTypes;

namespace RecipesBackend.DAL.Entities
{
    [BsonIgnoreExtraElements]
	public class Recipe : Entity<string>
	{
        public Recipe(string id) : base(id) { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public new string? Id { get; set; }

		[BsonElement("title")]
		public LocalizedString Title { get; set; } = new();

		public string? Image { get; set; } = "";
		public string? ImageType { get; set; } = "";

		// Tags
		public bool? Vegetarian { get; set; } = false;
		public bool? Vegan { get; set; } = false;
		public bool? GlutenFree { get; set; } = false;
		public bool? DairyFree { get; set; } = false;
		public bool? VeryHealthy { get; set; } = false;
		public bool? Cheap { get; set; } = false;
		public bool? VeryPopular { get; set; } = false;

		// CookingTime (MB TimeSpan instead double)
		public double? ReadyInMinutes { get; set; } = 0;
		public double? PreparationMinutes { get; set; } = 0;
		public double? CookingMinutes { get; set; } = 0;

		// Ingredients
		[BsonElement("extendedIngredients")]
		public List<Ingredient>? Ingredients { get; set; } = new();

		// Extra Info
		public int? AggregateLikes { get; set; } = 0;
		public int? Likes { get; set; } = 0;
		public int? HealthScore { get; set; } = 0;
		public int? Servings { get; set; } = 0;
		public int? WeightWatcherSmartPoints { get; set; } = 0;

		public Money? PricePerServing { get; set; } = new();

		// License
		public string? License { get; set; } = "";
		public string? SourceName { get; set; } = "";
		public string? SourceUrl { get; set; } = "";
		public string? SpoonacularSourceUrl { get; set; } = "";

		//TODO
		// Need to produce Entities below
		[BsonElement("analyzedInstructions")]
		public List<Instruction>? Instructions { get; set; } = new();

		// public List<string> Cuisines { get; set; }
		
		// TODO
		// Typo in calories
		public double? Callories { get; set; } = 0;
		public string? CalloriesUnits { get; set; } = "";

		public Nutrition? Nutrition { get; set; } = new();
	}
}