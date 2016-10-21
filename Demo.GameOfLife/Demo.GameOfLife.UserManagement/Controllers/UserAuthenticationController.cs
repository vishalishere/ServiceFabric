using System;
using System.Diagnostics.Eventing.Reader;
using System.Fabric;
using Demo.GameOfLife.UserManagement.BLL;
using System.Threading.Tasks;
using System.Web.Http;
using Demo.GameOfLife.Contracts;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    [ServiceRequestActionFilter]
    public class AuthenticationController : ApiController
    {
        //todo: move const to config
        private const string DemoGameofLifeUserAuthenticationServiceAddress = "fabric:/Demo.GameOfLife.UserAuthenticationService/UserAuthentication";
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

        private static IUserAuthentication GetAuthServiceInstance()
        {
            return ServiceProxy.Create<IUserAuthentication>(new Uri(DemoGameofLifeUserAuthenticationServiceAddress), new ServicePartitionKey(1));
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
