using RecipesBackend.DAL.Entities.ServiceEntities;
using System.Security.Cryptography.X509Certificates;

namespace RecipesBackend.DAL.Entities
{
    public class Recipe
    {
        // General Information
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }


        // Tags
        public bool IsVegeterian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsDairyFree { get; set; }
        public bool IsVeryHealthy { get; set; }
        public bool IsCheap { get; set; }
        public bool IsVeryPopular { get; set; }
        public bool IsSustainable { get; set; }
        public bool IsLowFodMap { get; set; }


        // CookingTime (MB TimeSpan instead double)
        public double ReadyInMinutes { get; set; }
        public double PreparationTimeMinutes { get; set; }
        public double CookingMinutes { get; set; }


        // Ingredients
        public List<Ingredient> ExtendedIngredients { get; set; }
        public List<Ingredient> UsedIngredients { get; set; }
        public List<Ingredient> MissedIngredients { get; set; }

        // Extra Info
        public string Gaps { get; set; }
        public int AggregateLikes { get; set; }
        public int Likes { get; set; } 
        public int HealthScore { get; set; }
        public int Servings { get; set; }
        public int WeightWatcherSmartPoints { get; set; }
        public double PricePerServing { get; set; }


        // License
        public string CreditsText { get; set; }
        public string License { get; set; }
        public string SourceName { get; set; }
        public string SourceURL { get; set; }
        public string SpoonAcularSourceURL { get; set; }


        //TODO
        // Need to produce Entities below
        public List<Instruction> AnalyzedInstructions { get; set; }
        public string[] Cuisines { get; set; }
        public string[] DishTypes { get; set; }
        public string[] Diets { get; set; }
        public string[] Occasions { get; set; }

    }
}
