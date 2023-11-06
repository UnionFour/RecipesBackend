using RecipesBackend.DAL.Entities;

namespace RecipesBackend.DAL.Repositories.RecipeRepository
{
    public interface IRecipeRepository : IDisposable
    {
        IEnumerable<Recipe> GetRecipes();
        Recipe GetRecipeById(int id);
        void CreateRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        void Save();
    }
}
