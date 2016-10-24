using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Demo.GameOfLife.Contracts;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Threading;
using Demo.GameOfLife.Engine.BL;
using Demo.GameOfLife.Engine.Model;

namespace Demo.GameOfLife.Engine
{
    internal sealed class Engine : StatefulService, IGameEngine
    {
        private const string BoardDictionaryKey = "AllGameBoards";
        private readonly BoardComputer _boardComputer;

        public Engine(StatefulServiceContext context)
            : base(context)
        {
            _boardComputer = new BoardComputer();
        }

        public async Task ComputeBoardFor(GameBoard board, Guid sessionToken)
        {
            var boards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, GameBoard>>(BoardDictionaryKey);
            using (var tx = StateManager.CreateTransaction())
            {
                await boards.AddAsync(tx, sessionToken, board);
                await tx.CommitAsync();
            }
        }

        public async Task<GameBoard> GetBoardResultsFor(Guid sessionToken)
        {
            var boards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, GameBoard>>(BoardDictionaryKey);
            using (var tx = StateManager.CreateTransaction())
            {
                var board = await boards.TryGetValueAsync(tx, sessionToken);
                return board.HasValue? board.Value : null;
            }
        }

        public async Task<bool> IsBoardComputationFinished(Guid sessionToken)
        {
            var boards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, GameBoard>>(BoardDictionaryKey);
            using (var tx = StateManager.CreateTransaction())
            {
                var board = await boards.TryGetValueAsync(tx, sessionToken);
                return board.HasValue && await _boardComputer.IsComputationFinished(board.Value);
            }
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[] { new ServiceReplicaListener(this.CreateServiceRemotingListener), };
        }

        /// <summary>
        /// Compute async all boards, one generation at a time.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

            var boards = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, GameBoard>>(BoardDictionaryKey);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = StateManager.CreateTransaction())
                {
                    var sessionEnumerator = (await boards.CreateEnumerableAsync(tx)).GetAsyncEnumerator();
                    while(await sessionEnumerator.MoveNextAsync(cancellationToken))
                    {
                        var session = sessionEnumerator.Current;
                        if (await _boardComputer.IsComputationFinished(session.Value))
                            continue;

                        await _boardComputer.ComputeGeneration(session.Value);
                        await boards.SetAsync(tx, session.Key, session.Value);
                        await tx.CommitAsync();
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
