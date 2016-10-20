using System.Security.Cryptography;
using System.Text;

namespace Demo.GameOfLife.UserManagement.BLL.Helpers
{
    public class HashHelper : IHashHelper
    {
        public byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
