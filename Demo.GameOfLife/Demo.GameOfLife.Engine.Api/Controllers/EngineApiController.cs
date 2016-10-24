using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Demo.GameOfLife.Contracts;
using Demo.GameOfLife.Engine.Model;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;

namespace Demo.GameOfLife.Engine.Api.Controllers
{
    [ServiceRequestActionFilter]
    public class EngineApiController : ApiController
    {
        //todo: move const to config
        private const string DemoGameofLifeEngineServiceAddress = "fabric:/Demo.GameOfLife.EngineService/Engine";

        [HttpGet]
        public async Task<IHttpActionResult> IsBoardComputationFinished(Guid sessionToken)
        {
            var result = await GameEngineInstance.IsBoardComputationFinished(sessionToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> ComputeBoardFor([FromBody]GameBoard board)
        {
            await GameEngineInstance.ComputeBoardFor(board, board.Token);
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBoardResultsFor(Guid sessionToken)
        {
            var result = await GameEngineInstance.GetBoardResultsFor(sessionToken);
            return Ok(result);
        }

        private IGameEngine GameEngineInstance 
            => ServiceProxy.Create<IGameEngine>(new Uri(DemoGameofLifeEngineServiceAddress), new ServicePartitionKey(1));
    }
}