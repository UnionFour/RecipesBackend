using HotChocolate.Data;
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
}