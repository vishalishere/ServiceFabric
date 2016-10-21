using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Demo.GameOfLife.Contracts
{
    public interface IUserStats : IService
    {
        Task<long> CurrentlyLoggedInUserCount();
    }
}
