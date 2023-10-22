var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddGraphQLServer();

var app = builder.Build();

app.MapGraphQL();

app.Run();