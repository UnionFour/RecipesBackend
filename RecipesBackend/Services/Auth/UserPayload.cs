namespace RecipesBackend.Services.Auth;

public class UserPayload
{
	public string Id { get; set; }
	public string Login { get; set; }
	public string Token { get; set; }
}