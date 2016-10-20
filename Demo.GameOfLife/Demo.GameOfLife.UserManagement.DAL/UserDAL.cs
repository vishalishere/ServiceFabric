using System.Data.Entity;
using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public class UserDAL: IUserDAL
    {
        private IUserManagementConnection _context;

        public UserDAL(IUserManagementConnection context)
        {
            _context = context;
        }

        public UserDAL() : this(new UserManagementConnection()) { }

        public async Task<User> GetBy(string username, string passwordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == passwordHash.ToLower());
        }
    }
}
