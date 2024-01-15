using System.Security.Claims;
using HotChocolate.Authorization;
using HotChocolate.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Types.Queries;

[QueryType]
public class Query
{
	[UsePaging(IncludeTotalCount = true)]
	[UseFiltering]
	[UseSorting]
	// ReSharper disable once UnusedMember.Global
	public IExecutable<Recipe> GetRecipes([Service] IMongoCollection<Recipe> collection) =>
		collection.AsExecutable();

	[UseFiltering]
	// ReSharper disable once UnusedMember.Global
	public IExecutable<IngredientCollection> GetIngredients([Service] IMongoCollection<IngredientCollection> collection)
	{
		var sortDefinition = Builders<IngredientCollection>.Sort.Descending(ingredientCollection => ingredientCollection.Count);
		var filterDefinition = Builders<IngredientCollection>.Filter.Empty;

		return collection.Find(filterDefinition, new FindOptions())
			.Sort(sortDefinition)
			.AsExecutable();
	}
	
	[Authorize]
	public async Task<List<Recipe>> GetFavouriteRecipes(
		ClaimsPrincipal claimsPrincipal,
		[FromServices] IMongoCollection<User> users,
		[FromServices] IMongoCollection<Recipe> recipes)
	{
		var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
		var user = await users.Find(user => user.Email == email).FirstAsync();

		var result = new List<Recipe>();
		foreach (var likedRecipe in user.LikedRecipes)
		{
			var recipe = await recipes.Find(r => r.Id == likedRecipe).FirstAsync();
			
			result.Add(recipe);
		}

		return result;
	}
}