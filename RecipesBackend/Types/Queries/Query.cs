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
}