using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using RecipesBackend;
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

// MongoDB configuration
var connectionString = builder.Configuration.GetConnectionString("Mongo");
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

builder.Services.AddSingleton(sp => database.GetCollection<Recipe>("Recipes"));
builder.Services.AddSingleton(sp => database.GetCollection<IngredientCollection>("Ingredients"));

builder.Services
	.AddGraphQLServer()
	.AddTypes()
	.AddFiltering<CustomFilteringConvention>()	
	.AddMongoDbSorting()
	.AddMongoDbProjections()
	.AddMongoDbPagingProviders();

var app = builder.Build();

app.MapGraphQL();

app.UseCors();

app.Run();