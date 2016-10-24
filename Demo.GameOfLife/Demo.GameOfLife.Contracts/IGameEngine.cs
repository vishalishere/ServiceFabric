using System;
using System.Threading.Tasks;
using Demo.GameOfLife.Engine.Model;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Demo.GameOfLife.Contracts
{
    public interface IGameEngine : IService
    {
        Task<bool> IsBoardComputationFinished(Guid sessionToken);
        Task ComputeBoardFor(GameBoard board, Guid sessionToken);
        Task<GameBoard> GetBoardResultsFor(Guid sessionToken);
    }
}