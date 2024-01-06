using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesBackend.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Recipe
	{
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("title")]
		public LocalizedString Title { get; set; } = new();

		public string? Image { get; set; } = "";
		public string ImageType { get; set; } = "";

		// Tags
		public bool Vegetarian { get; set; }
		public bool Vegan { get; set; }
		public bool GlutenFree { get; set; }
		public bool DairyFree { get; set; }
		public bool VeryHealthy { get; set; }
		public bool Cheap { get; set; }
		public bool VeryPopular { get; set; }

		// CookingTime (MB TimeSpan instead double)
		public double ReadyInMinutes { get; set; }
		public double PreparationMinutes { get; set; }
		public double CookingMinutes { get; set; }

		// Ingredients
		[BsonElement("extendedIngredients")]
		public List<Ingredient>? Ingredients { get; set; } = new();

		// Extra Info
		public int AggregateLikes { get; set; }
		public int Likes { get; set; }
		public int HealthScore { get; set; }
		public int Servings { get; set; }
		public int WeightWatcherSmartPoints { get; set; }

		public Money PricePerServing { get; set; } = new();

		// License
		public string License { get; set; } = "";
		public string SourceName { get; set; } = "";
		public string SourceUrl { get; set; } = "";
		public string SpoonacularSourceUrl { get; set; } = "";

		//TODO
		// Need to produce Entities below
		[BsonElement("analyzedInstructions")]
		public List<Instruction> Instructions { get; set; } = new();

		// public List<string> Cuisines { get; set; }
		
		// TODO
		// Typo in calories
		public double Callories { get; set; }
		public string CalloriesUnits { get; set; }
		
		public Nutrition Nutrition { get; set; }
	}
}