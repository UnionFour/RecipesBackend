using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace RecipesBackend;

public static class MongoDatabaseExtensions
{
	public static IMongoDatabase GetRecipeDatabase(string connectionString)
	{
		// MongoDB configuration
		var mongoConnectionUrl = new MongoUrl(connectionString);
		var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

		mongoClientSettings.ClusterConfigurator = cb =>
		{
			// This will print the executed command to the console
			cb.Subscribe<CommandStartedEvent>(e => { Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}"); });
			cb.Subscribe<CommandFailedEvent>(e => { Console.WriteLine($"{e.CommandName} - fail {e.ToJson()}"); });
		};

		var pack = new ConventionPack();
		pack.Add(new CamelCaseElementNameConvention());

		ConventionRegistry.Register("camel case", pack, t => true);

		var client = new MongoClient(mongoClientSettings);
		return client.GetDatabase("Recipes");
	}
}