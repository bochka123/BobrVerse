using BobrVerse.Auth.Interfaces;

namespace BobrVerse.Auth.Services
{
    public class UserContextService: IUserContextService
    {
        public Guid UserId { get; private set; }

        void IUserContextService.SetUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
