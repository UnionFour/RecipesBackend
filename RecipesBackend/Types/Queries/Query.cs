using HotChocolate.Data;
using MongoDB.Driver;
using RecipesBackend.DAL.Entities;

namespace RecipesBackend.Types.Queries;

[QueryType]
public class Query
{
	[UsePaging]
	[UseFiltering]
	[UseSorting]
	public IExecutable<Recipe> GetRecipes([Service] IMongoCollection<Recipe> collection) =>
		collection.AsExecutable();
}