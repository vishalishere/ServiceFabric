using System.Data.Entity;
using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public interface IUserManagementConnection
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();
    }
}