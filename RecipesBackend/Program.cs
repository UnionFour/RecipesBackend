using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using RecipesBackend.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services.AddSingleton(sp =>
{
	const string connectionString = "mongodb://localhost:27017";
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
	var database = client.GetDatabase("Recipes");

	return database.GetCollection<Recipe>("Recipes");
});

builder.Services
	.AddGraphQLServer()
	.AddTypes()
	.AddMongoDbFiltering()
	.AddMongoDbSorting()
	.AddMongoDbProjections()
	.AddMongoDbPagingProviders();

var app = builder.Build();

app.MapGraphQL();

app.UseCors();

app.Run();