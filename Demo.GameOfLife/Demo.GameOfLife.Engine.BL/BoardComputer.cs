using System.Threading.Tasks;
using Demo.GameOfLife.Engine.Model;

namespace Demo.GameOfLife.Engine.BL
{
    public class BoardComputer
    {
        public Task<bool> IsComputationFinished(GameBoard board)
        {
            return Task.FromResult(false);
        }

        public Task ComputeGeneration(GameBoard board)
        {
            throw new System.NotImplementedException();
        }
    }
    
}
