using System;
using System.Fabric;

namespace Demo.GameOfLife.UserManagement.DAL
{
    public class BaseDal
    {
        protected IUserManagementConnection Context()
        {
            return new UserManagementConnection(ConnectionString);
        }

        protected static string ConnectionString
        {
            get
            {
                var activationContext = FabricRuntime.GetActivationContext();
                var configurationPackage = activationContext.GetConfigurationPackageObject("Config");
                return configurationPackage.Settings.Sections["DatabaseConnection"].Parameters["DatabaseConnectionString"].Value;
            }
        }
    }
}