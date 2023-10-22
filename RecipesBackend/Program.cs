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

builder.Services
	.AddGraphQLServer()
	.AddTypes();

var app = builder.Build();

app.MapGraphQL();

app.UseCors();

app.Run();