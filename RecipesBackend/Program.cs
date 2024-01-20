using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
var connectionString = builder.Configuration.GetConnectionString("Mongo") ??
                       throw new Exception("ConnectionString for mongo not found");
var database = MongoDatabaseExtensions.GetRecipeDatabase(connectionString);

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
builder.Services.AddSingleton(sp => database.GetCollection<User>("Users"));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services
	.AddGraphQLServer()
	.AddAuthorization()
	.AddProjections()
	.AddTypes()
	.AddFiltering<CustomFilteringConvention>()
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