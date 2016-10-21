using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public interface IUserManagementConnection: IDisposable
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();
    }
}