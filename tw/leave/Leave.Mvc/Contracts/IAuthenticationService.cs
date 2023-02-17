using Leave.Mvc.Models;

namespace Leave.Mvc.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM registration);
        Task Logout();
    }
}