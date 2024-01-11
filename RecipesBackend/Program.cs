using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using RecipesBackend;
using RecipesBackend.DAL.Entities;
using RecipesBackend.Services.Auth;

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

// authorization
var authSection = builder.Configuration.GetSection("Auth");
var authOptions = authSection.Get<AuthOptions>();

builder.Services.Configure<AuthOptions>(authSection);

builder.Services.AddDataProtection();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions?.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions?.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions?.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

builder.Services.AddSingleton(sp => database.GetCollection<Recipe>("Recipes"));
builder.Services.AddSingleton(sp => database.GetCollection<IngredientCollection>("Ingredients"));

builder.Services
	.AddGraphQLServer()
	.AddAuthorization()
	.AddProjections()
	.AddTypes()
	.AddFiltering<CustomFilteringConvention>()
	.AddSorting()
    .AddMutationConventions()
    .AddMongoDbSorting()
	.AddMongoDbProjections()
	.AddMongoDbPagingProviders();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.UseCors();

app.Run();