using System;
using System.Web.Mvc;
using Demo.GameOfLife.Contracts;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Demo.GameOfLife.WebApp.Controllers
{
    public class AuthenticaticatedBaseController : Controller
    {
        //todo: move const to config
        private const string DemoGameofLifeUserAuthenticationServiceAddress = "fabric:/Demo.GameOfLife.UserAuthenticationService/UserAuthentication";

        protected static IUserAuthentication GetAuthServiceInstance()
        {
            return ServiceProxy.Create<IUserAuthentication>(new Uri(DemoGameofLifeUserAuthenticationServiceAddress), new ServicePartitionKey(1));
        }
    }
}