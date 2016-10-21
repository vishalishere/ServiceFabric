namespace Demo.GameOfLife.UserManagement.DAL
{
    public partial class UserManagementConnection: IUserManagementConnection
    {
        public UserManagementConnection(string connectionString):base(connectionString)
        {
            
        }
    }
}
