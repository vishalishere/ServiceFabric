using System.Threading.Tasks;

namespace Demo.GameOfLife.Contracts
{
    public interface IUserStats
    {
        Task<long> CurrentlyLoggedInUserCount();
    }
}
