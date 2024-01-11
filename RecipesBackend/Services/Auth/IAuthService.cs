namespace Auth
{
    public interface IAuthService
    {
        public string RegisterUser(UserInput input);

        public string AuthorizeUser(UserInput input);
    }
}