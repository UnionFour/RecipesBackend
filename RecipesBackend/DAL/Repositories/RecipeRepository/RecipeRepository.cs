using RecipesBackend.DAL.DBContext;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.DAL.Repositories.RecipeRepository
{
    public class RecipeRepository : IRecipeRepository
    {
        private ProjectDBContext _dbContext;

        private bool isDisposed = false;

        public RecipeRepository(ProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Recipe> GetRecipes() => _dbContext.recipes;
        public async Task<IEnumerable<Recipe>> GetRecipesAsync() => await Task.Run(() => GetRecipes());

        public Recipe GetRecipeById(int id) => _dbContext.recipes.FirstOrDefault(x => x.Id == id);
        public async Task<Recipe> GetRecipeByIdAsync(int id) => await Task.Run(() => GetRecipeById(id));

        public void CreateRecipe(Recipe recipe) => _dbContext.recipes.Add(recipe);
        public async void CreateRecipeAsync(Recipe recipe) => await Task.Run(() => CreateRecipe(recipe));

        public void UpdateRecipe(Recipe recipe) => _dbContext.recipes.Update(recipe);
        public async void UpdateRecipeAsync(Recipe recipe) => await Task.Run(() => UpdateRecipe(recipe));

        public void DeleteRecipe(Recipe recipe) => _dbContext.recipes.Remove(recipe);
        public async void DeleteRecipeAsync(Recipe recipe) => await Task.Run(() => DeleteRecipe(recipe));

        public void Save() => _dbContext.SaveChanges();
        public async void SaveAsync() => await Task.Run(() => Save());

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
