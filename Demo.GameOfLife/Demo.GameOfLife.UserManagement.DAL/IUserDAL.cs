using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public interface IUserDAL
    {
        Task<User> GetBy(string username, string passwordHash);
        Task Add(User user);
    }
}