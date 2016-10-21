using System;
using System.Web.Http;
using Demo.GameOfLife.Contracts;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Demo.GameOfLife.UserManagement.Controllers
{
    public class AuthenticationServiceBaseController : ApiController
    {
        //todo: move const to config
        private const string DemoGameofLifeUserAuthenticationServiceAddress = "fabric:/Demo.GameOfLife.UserAuthenticationService/UserAuthentication";

        protected static IUserAuthentication GetAuthServiceInstance()
        {
            return ServiceProxy.Create<IUserAuthentication>(new Uri(DemoGameofLifeUserAuthenticationServiceAddress), new ServicePartitionKey(1));
        }

        protected static IUserStats GetUserStatsServiceInstance()
        {
            return ServiceProxy.Create<IUserStats>(new Uri(DemoGameofLifeUserAuthenticationServiceAddress), new ServicePartitionKey(1));
        }
    }
}