using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Demo.GameOfLife.Contracts
{
    public interface IUserAuthentication: IService
    {
        Task<bool> ValidateUserSessionToken(string username, Guid token);
        Task<Guid> AddUserSession(string username);
    }
}
