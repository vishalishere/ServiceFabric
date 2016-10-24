using System.Collections.Generic;
using System.Linq;
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
            ApplyGameRules(board.Board);

            //TODO parallelize computation
            return Task.FromResult(1);
        }

        public void ApplyGameRules(IEnumerable<BoardCell> board)
        {
            foreach (var boardCell in board)
            {
                var aliveCellCount = 0;
                BoardCell cell = null;
                if ((cell = GetNorthWestNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetNorthNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetNorthEastNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetWestNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetEastNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetSouthWestNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetSouthNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;

                if ((cell = GetSouthEastNeighbour(board, boardCell)) != null && cell.IsAlive)
                    aliveCellCount++;


                ApplyRuleWhenLessThanTwoLiveNeighboursReturnDead(boardCell, aliveCellCount);
                ApplyRuleWhenMoreThanThreeLiveNeighboursReturnsDead(boardCell, aliveCellCount);
                ApplyRuleWhenTwoOrThreeLiveNeighboursAndCellLiveReturnsAlive(boardCell, aliveCellCount);
                ApplyRuleWhenThreeLiveNeighboursAndCellDeadRturnsAlive(boardCell, aliveCellCount);
            }
        }

        private static void ApplyRuleWhenLessThanTwoLiveNeighboursReturnDead(BoardCell boardCell, int aliveCellCount)
        {
            boardCell.IsAlive = !(aliveCellCount < 2);
        }

        private static void ApplyRuleWhenMoreThanThreeLiveNeighboursReturnsDead(BoardCell boardCell, int aliveCellCount)
        {
            boardCell.IsAlive = !(aliveCellCount > 3);
        }
        
        private static void ApplyRuleWhenTwoOrThreeLiveNeighboursAndCellLiveReturnsAlive(BoardCell boardCell, int aliveCellCount)
        {
            boardCell.IsAlive = boardCell.IsAlive && (aliveCellCount == 2 || aliveCellCount == 3);
        }
        
        private static void ApplyRuleWhenThreeLiveNeighboursAndCellDeadRturnsAlive(BoardCell boardCell, int aliveCellCount)
        {
            boardCell.IsAlive = !boardCell.IsAlive && aliveCellCount == 3;
        }


        private static BoardCell GetLeftNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell, int rowIndex)
        {
            return board.SingleOrDefault(x => x.XPosition == boardCell.XPosition - 1 && x.YPosition == boardCell.YPosition + rowIndex);
        }

        private static BoardCell GetCentralNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell, int rowIndex)
        {
            return board.SingleOrDefault(x => x.XPosition == boardCell.XPosition && x.YPosition == boardCell.YPosition + rowIndex);
        }

        private static BoardCell GetRightNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell, int rowIndex)
        {
            return board.SingleOrDefault(x => x.XPosition == boardCell.XPosition + 1 && x.YPosition == boardCell.YPosition + rowIndex);
        }

        private static BoardCell GetNorthWestNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetLeftNeighbour(board, boardCell, -1);
        }

        private static BoardCell GetNorthNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetCentralNeighbour(board, boardCell, -1);
        }

        private static BoardCell GetNorthEastNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetRightNeighbour(board, boardCell, - 1);
        }

        private static BoardCell GetSouthWestNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetLeftNeighbour(board, boardCell, -1);
        }

        private static BoardCell GetSouthNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetCentralNeighbour(board, boardCell, -1);
        }

        private static BoardCell GetSouthEastNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetRightNeighbour(board, boardCell, - 1);
        }
        private static BoardCell GetWestNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetLeftNeighbour(board, boardCell, 0);
        }
        
        private static BoardCell GetEastNeighbour(IEnumerable<BoardCell> board, BoardCell boardCell)
        {
            return GetRightNeighbour(board, boardCell, 0);
        }
    }
    
}
