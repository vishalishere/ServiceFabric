using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Demo.GameOfLife.Contracts;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Demo.GameOfLife.UserAuthentication
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class UserAuthentication : StatefulService, IUserAuthentication, IUserStats
    {
        private const string UserSessionDictKey = "userSession";

        public UserAuthentication(StatefulServiceContext context)
            : base(context)
        { }


        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new [] { new ServiceReplicaListener(this.CreateServiceRemotingListener), };
        }

        public async Task<bool> ValidateUserSessionToken(string username, Guid token)
        {
            var sessionDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, UserSession>>(UserSessionDictKey);

            using (var tx = this.StateManager.CreateTransaction())
            {
                var session = await sessionDictionary.TryGetValueAsync(tx, username);

                if (!session.HasValue)
                {
                    ServiceEventSource.Current.ServiceMessage(Context, "User does not exist");
                    return false;
                }

                if (session.Value.ExpireOn.Ticks < DateTime.UtcNow.Ticks)
                {
                    ServiceEventSource.Current.ServiceMessage(Context, "User session expired.");
                    return false;
                }

                session.Value.RenewExpireTime();
                await sessionDictionary.SetAsync(tx, username, session.Value);
                await tx.CommitAsync();

                return true;
            }
        }

        public async Task<Guid> AddUserSession(string username)
        {
            var sessionDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, UserSession>>(UserSessionDictKey);

            using (var tx = StateManager.CreateTransaction())
            {
                var userSession = new UserSession(username);
                await sessionDictionary.AddAsync(tx, username, userSession);
                await tx.CommitAsync();


                ServiceEventSource.Current.ServiceMessage(Context, "User {0} added with session id: {1}",
                    username, userSession.UserToken);

                return userSession.UserToken;
            }
        }

        public async Task<long> CurrentlyLoggedInUserCount()
        {
            var sessionDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, UserSession>>(UserSessionDictKey);

            using (var tx = StateManager.CreateTransaction())
            {
                return await sessionDictionary.GetCountAsync(tx);
            }
        }
    }
}
