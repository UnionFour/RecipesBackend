using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Types.Mutations;

[MutationType]
public class Mutation
{
	// TODO: Реализовать AddRecipe
	// - Добавление рецепта, проверка на имя (если уже такое существует)
	// -	- Добавление нового id
	// -	- Если какого-то ингредиента нет в БД по имени
	// -	-	- Добавление в коллекцию ingredients, count = 1
	// -	- Иначе замена ингредиента 
	// -	-	- Замена id, image, imageType
	// -	- Добавление инструкций
	// -	-	- Добавляем ингредиенты только из extendedIngredients (делаем проверку)
	// -	-	- Проставляем шаги (number)
	public async Task<Recipe> AddRecipe([FromServices] IMongoCollection<Recipe> recipeCollection,
		[FromServices] IMongoCollection<IngredientCollection> ingredientCollection,
		Recipe recipe)
	{
		recipe.Id = ObjectId.GenerateNewId().ToString();

		foreach (var ingredient in recipe.Ingredients ?? Enumerable.Empty<Ingredient>())
		{
			if (ingredient.Name == null || ingredient.Name.Rus == "")
				throw new Exception("Пустое имя ингредиента");

			var ingredientExists = await recipeCollection
				.Find($"{{ extendedIngredients: {{ $elemMatch: {{ 'nameClean.rus': '{ingredient.Name.Rus}' }} }} }}")
				.AnyAsync();

			// TODO: Вынести в отдельный метод работы с коллекцией ингредиентов
			if (ingredientExists)
			{
				await ingredientCollection.UpdateOneAsync(
					Builders<IngredientCollection>.Filter.Eq("_id", ingredient.Name.Rus),
					"{ $inc: { count: 1 } }");
			}
		}

		// TODO: Refactoring
		foreach (var instruction in recipe.Instructions)
		{
			foreach (var (step, i) in instruction.Steps.Select((s, i) => (s, i)))
			{
				step.Number = i + 1;
				
				foreach (var stepIngredient in step.Ingredients ?? Enumerable.Empty<Ingredient>())
				{
					if (recipe.Ingredients?.All(i => i.Name?.Rus != stepIngredient.Name?.Rus) ?? false)
						recipe.Ingredients.Add(stepIngredient);
				}
			}
		}

		await recipeCollection.InsertOneAsync(recipe);
		return recipe;
	}
}