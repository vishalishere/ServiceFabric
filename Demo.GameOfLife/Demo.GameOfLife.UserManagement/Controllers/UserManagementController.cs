using Demo.GameOfLife.UserManagement.BLL;
using Demo.GameOfLife.UserManagement.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class UserManagementController : ApiController
    {
        private IUserManagement _userManagement;

        public UserManagementController(IUserManagement userManagement) 
        {
            _userManagement = userManagement;
        }

        public UserManagementController() : this(new BLL.UserManagement())
        {

        }

        // POST api/values 
        public async Task Post([FromBody]User user)
        {
            await _userManagement.AddUser(user.DisplayName, user.Username, user.Password);
        }
    }
}
