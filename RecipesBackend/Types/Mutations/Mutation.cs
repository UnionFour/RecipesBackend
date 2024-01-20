using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;
using RecipesBackend.DAL.ValueTypes;
using RecipesBackend.Services.Auth;

namespace RecipesBackend.Types.Mutations;

[MutationType]
public class Mutation
{
	public UserPayload RegisterUser(
		[Service] IAuthService authService,
		UserAuth input) => authService.RegisterUser(input);

	public UserPayload AuthorizeUser(
		[Service] IAuthService authService,
		UserAuth input) => authService.AuthorizeUser(input);

	public async Task<Recipe> AddRecipe([FromServices] IMongoCollection<Recipe> recipeCollection,
		[FromServices] IMongoCollection<IngredientCollection> ingredientCollection,
		Recipe recipe)
	{
		recipe.Id = ObjectId.GenerateNewId().ToString();

		// TODO: Refactoring
		foreach (var instruction in recipe.Instructions ?? Enumerable.Empty<Instruction>())
		{
			foreach (var (step, i) in instruction.Steps.Select((s, i) => (s, i)))
			{
				step.Number = i + 1;

				foreach (var stepIngredient in step.Ingredients ?? Enumerable.Empty<Ingredient>())
				{
					if (recipe.Ingredients?.All(ing => ing.Name?.Rus != stepIngredient.Name?.Rus) ?? false)
						recipe.Ingredients.Add(stepIngredient);
				}
			}
		}

		foreach (var ingredient in recipe.Ingredients ?? Enumerable.Empty<Ingredient>())
		{
			if (ingredient.Name?.Rus is null or "")
				throw new Exception("Пустое имя ингредиента");

			var ingredientExist = await ingredientCollection
				.Find($"{{ _id: '{ingredient.Name.Rus}' }}")
				.AnyAsync();

			// TODO: Вынести в отдельный метод работы с коллекцией ингредиентов
			if (ingredientExist)
				await ingredientCollection.UpdateOneAsync(
					$"{{_id: '{ingredient.Name.Rus}'}}",
					"{ $inc: { count: 1 } }");
			else
				await ingredientCollection.InsertOneAsync(new IngredientCollection()
				{
					Id = ingredient.Name.Rus
				});
		}

		await recipeCollection.InsertOneAsync(recipe);
		return recipe;
	}

	public async Task<Recipe> UpdateRecipe([FromServices] IMongoCollection<Recipe> recipeCollection,
		[FromServices] IMongoCollection<IngredientCollection> ingredientCollection,
		Recipe recipe)
	{
		var recipeBsonDocument = recipe.ToBsonDocument();

		// TODO: Refactoring
		foreach (var instruction in recipe.Instructions ?? Enumerable.Empty<Instruction>())
		{
			foreach (var (step, i) in instruction.Steps.Select((s, i) => (s, i)))
			{
				step.Number = i + 1;

				foreach (var stepIngredient in step.Ingredients ?? Enumerable.Empty<Ingredient>())
				{
					if (recipe.Ingredients?.All(ing => ing.Name?.Rus != stepIngredient.Name?.Rus) ?? false)
						recipe.Ingredients.Add(stepIngredient);
				}
			}
		}

		foreach (var ingredient in recipe.Ingredients ?? Enumerable.Empty<Ingredient>())
		{
			if (ingredient.Name?.Rus is null or "")
				throw new Exception("Пустое имя ингредиента");

			var ingredientExist = await ingredientCollection
				.Find($"{{ _id: '{ingredient.Name.Rus}' }}")
				.AnyAsync();

			// TODO: Вынести в отдельный метод работы с коллекцией ингредиентов
			if (ingredientExist)
				await ingredientCollection.UpdateOneAsync(
					$"{{_id: '{ingredient.Name.Rus}'}}",
					"{ $inc: { count: 1 } }");
			else
				await ingredientCollection.InsertOneAsync(new IngredientCollection
				{
					Id = ingredient.Name.Rus
				});
		}

		await recipeCollection.UpdateOneAsync($"{{ _id: ObjectId('{recipe.Id}') }}", $"{{ $set: {recipeBsonDocument} }}");
		return recipe;
	}

	public async Task<string> RenameIngredient([FromServices] IMongoCollection<Recipe> recipeCollection,
		[FromServices] IMongoCollection<IngredientCollection> ingredientCollection,
		string ingredientName, string newIngredientName)
	{
		var ingredientExist = await ingredientCollection.Find($"{{ _id: '{ingredientName}' }}").AnyAsync();

		if (!ingredientExist)
			throw new Exception("Такого ингредиента нет");

		await ingredientCollection.DeleteOneAsync($"{{ _id: '{ingredientName}' }}");
		await ingredientCollection.InsertOneAsync(new IngredientCollection
		{
			Id = newIngredientName
		});

		await recipeCollection.UpdateManyAsync($"{{ extendedIngredients: {{ $elemMatch: {{ 'nameClean.rus': '{ingredientName}' }} }} }}",
			$"{{ $set: {{ 'extendedIngredients.$[element].nameClean.rus': '{newIngredientName}' }} }}",
			new UpdateOptions
			{
				ArrayFilters = new[]
					{ new BsonDocumentArrayFilterDefinition<Ingredient>(new BsonDocument("element.nameClean.rus", ingredientName)) }
			});

		return newIngredientName;
	}

	[Authorize]
	public async Task<Recipe> AddFavouriteRecipe(
		ClaimsPrincipal claimsPrincipal,
		[FromServices] IMongoCollection<User> users,
		[FromServices] IMongoCollection<Recipe> recipes,
		string id)
	{
		var recipe = await recipes.Find(recipe => recipe.Id == id).FirstAsync();

		var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
		var user = await users.Find(u => u.Email == email).FirstAsync();

		user.LikedRecipes.Add(id);

		await users.UpdateOneAsync(user => user.Email == email, Builders<User>.Update.Set(user => user.LikedRecipes, user.LikedRecipes));

		return recipe;
	}
}