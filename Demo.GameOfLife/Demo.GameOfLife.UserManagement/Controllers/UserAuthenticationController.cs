using System;
using Demo.GameOfLife.UserManagement.BLL;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class AuthenticationController : AuthenticationServiceBaseController
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
        public async Task<IHttpActionResult> ValidateUser(string username, string password)
        {
            var isValid = await _userManagement.IsUserPasswordValid(username, password);
            if (!isValid)
                return Unauthorized();

            var token = await GetAuthServiceInstance().AddUserSession(username);
            return Ok(token.ToString());
        }

        [HttpGet]
        public async Task<IHttpActionResult> IsValidSession(string username, string token)
        {
            var result =  await GetAuthServiceInstance().ValidateUserSessionToken(username, Guid.Parse(token));
            if (result)
                return Ok();
            else
                return Unauthorized();
        }
    }
}
