using System;
using System.Runtime.Serialization;

namespace Demo.GameOfLife.UserAuthentication
{
    [DataContract]
    internal class UserSession
    {
        private const int MaxExpireTimeInMins = 30;

        public UserSession(string username)
        {
            Username = username;
            UserToken = Guid.NewGuid();
            RenewExpireTime();
        }

        public void RenewExpireTime()
        {
            ExpireOn = new TimeSpan(DateTime.UtcNow.AddMinutes(MaxExpireTimeInMins).Ticks);
        }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public Guid UserToken { get; set; }

        [DataMember]
        public TimeSpan ExpireOn { get; set; }
    }
}