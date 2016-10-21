using Demo.GameOfLife.UserManagement.BLL;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class AuthenticationController : ApiController
    {
        private readonly IUserManagement _userManagement;

        public AuthenticationController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        public AuthenticationController() : this(new BLL.UserManagement())
        {

        }

        [HttpGet]
        public async Task<IHttpActionResult> IsValid(string username, string password)
        {
            var result = await _userManagement.IsUserPasswordValid(username, password);
            return Ok(result);
        }
    }
}
