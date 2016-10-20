using System.Data.Entity;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public interface IUserManagementConnection
    {
        DbSet<User> Users { get; set; }
    }
}