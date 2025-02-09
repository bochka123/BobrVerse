namespace BobrVerse.Auth.Interfaces
{
    public interface IUserContextService
    {
        Guid UserId { get; }
        internal void SetUser(Guid userId);
    }
}
