using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.BLL
{
    public interface IUserManagement
    {
        Task<bool> IsUserPasswordValid(string username, string password);
        Task AddUser(string displayName, string username, string password);
    }
}