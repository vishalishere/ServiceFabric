using System.Data.Entity;
using System.Threading.Tasks;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public class UserDal : BaseDal, IUserDAL
    {
        public async Task<User> GetBy(string username, string passwordHash)
        {
            using (var context = Context())
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == passwordHash.ToLower());
                return user;
            }
        }

        public async Task Add(User user)
        {
            using (var context = Context())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
