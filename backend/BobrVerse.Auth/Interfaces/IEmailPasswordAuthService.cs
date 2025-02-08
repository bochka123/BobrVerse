namespace BobrVerse.Auth.Interfaces
{
    public interface IEmailPasswordAuthService
    {
        Task RegisterAsync(string email, string password);
        Task LoginAsync(string email, string password);
        void Logout();
    }
}
