using System;
using System.Threading.Tasks;
using System.Web.Http;
using Demo.GameOfLife.UserManagement.BLL.Helpers;
using Demo.GameOfLife.UserManagement.DAL;


namespace Demo.GameOfLife.UserManagement.BLL
{
    public class UserManagement : IUserManagement
    {
        private IHashHelper _hashHelper;
        private IUserDAL _userDal;

        public UserManagement(IUserDAL userDal, IHashHelper hashHelper)
        {
            _userDal = userDal;
            _hashHelper = hashHelper;
        }
        public UserManagement(): this(new UserDal(), new HashHelper())
        {

        }

        [HttpGet]
        public async Task AddUser(string displayName, string username, string password)
        {
            var passwordHash = _hashHelper.GetHashString(password);
            await _userDal.Add(new User
            {
                Username = username,
                DisplayName = displayName,
                Password = passwordHash
            });
        }

        [HttpGet]
        public async Task<bool> IsUserPasswordValid(string username, string password)
        {
            var passwordHash = _hashHelper.GetHashString(password);
            return await _userDal.GetBy(username, passwordHash) != null;
        }
    }
}
