using Demo.GameOfLife.UserManagement.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class AuthenticationController : ApiController
    {
        private IUserManagement _userManagement;

        public AuthenticationController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public AuthenticationController() : this(new BLL.UserManagement())
        {

        }

        public async Task<bool> IsValid(string username, string password)
        {
            return await _userManagement.IsUserPasswordValid(username, password);
        }
    }
}
