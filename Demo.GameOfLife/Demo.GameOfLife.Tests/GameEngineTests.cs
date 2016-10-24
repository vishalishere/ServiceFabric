using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.GameOfLife.Engine.BL;
using Demo.GameOfLife.Engine.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.GameOfLife.Tests
{
    [TestClass]
    public class GameEngineTests
    {
        private BoardComputer _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new BoardComputer();
        }

        [TestMethod]
        public async Task IsComputationFinished_WhenRulesAreValid_ReturnsFalse()
        {
            GameBoard someBoard = null;

            var result = await _sut.IsComputationFinished(someBoard);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task ComputeGeneration_ForCellWithFewerThanTwoLiveNeighbours_CellDies()
        {

            var someBoard = new GameBoard
            {
                Board = new List<BoardCell>
                {
                    new BoardCell(0, 0, false),
                    new BoardCell(1, 0, true),
                    new BoardCell(2, 0, false),

                    new BoardCell(0, 1, false),
                    new BoardCell(1, 1, true),
                    new BoardCell(2, 1, false),

                    new BoardCell(0, 2, false),
                    new BoardCell(1, 2, false),
                    new BoardCell(2, 2, false),
                }
            };

            await _sut.ComputeGeneration(someBoard);

            Assert.IsFalse(someBoard.Board.Single(c => c.XPosition == 1 && c.YPosition == 1).IsAlive);
        }

        [TestMethod]
        public async Task ComputeGeneration_ForCellWithMoreThanThreeLiveNeighbours_CellDies()
        {

            var someBoard = new GameBoard
            {
                Board = new List<BoardCell>
                {
                    new BoardCell(0, 0, true),
                    new BoardCell(1, 0, true),
                    new BoardCell(2, 0, false),

                    new BoardCell(0, 1, false),
                    new BoardCell(1, 1, true),
                    new BoardCell(2, 1, true),

                    new BoardCell(0, 2, false),
                    new BoardCell(1, 2, true),
                    new BoardCell(2, 2, false),
                }
            };

            await _sut.ComputeGeneration(someBoard);

            Assert.IsFalse(someBoard.Board.Single(c => c.XPosition == 1 && c.YPosition == 1).IsAlive);
        }

        [TestMethod]
        public async Task ComputeGeneration_ForCellWithTwoOrThreeLiveNeighbours_CellLives()
        {

            var someBoard = new GameBoard
            {
                Board = new List<BoardCell>
                {
                    new BoardCell(0, 0, true),
                    new BoardCell(1, 0, true),
                    new BoardCell(2, 0, false),

                    new BoardCell(0, 1, false),
                    new BoardCell(1, 1, true),
                    new BoardCell(2, 1, true),

                    new BoardCell(0, 2, false),
                    new BoardCell(1, 2, false),
                    new BoardCell(2, 2, false),
                }
            };

            await _sut.ComputeGeneration(someBoard);

            Assert.IsFalse(someBoard.Board.Single(c => c.XPosition == 1 && c.YPosition == 1).IsAlive);
        }

        [TestMethod]
        public async Task ComputeGeneration_ForDeadCellWithThreeLiveNeighbours_CellReturnsAlive()
        {

            var someBoard = new GameBoard
            {
                Board = new List<BoardCell>
                {
                    new BoardCell(0, 0, true),
                    new BoardCell(1, 0, true),
                    new BoardCell(2, 0, false),

                    new BoardCell(0, 1, false),
                    new BoardCell(1, 1, false),
                    new BoardCell(2, 1, true),

                    new BoardCell(0, 2, false),
                    new BoardCell(1, 2, false),
                    new BoardCell(2, 2, false),
                }
            };

            await _sut.ComputeGeneration(someBoard);

            Assert.IsFalse(someBoard.Board.Single(c => c.XPosition == 1 && c.YPosition == 1).IsAlive);
        }

        [TestMethod]
        public async Task ComputeGeneration_ForCornerCellWithThreeLiveNeighbours_CellReturnsAlive()
        {

            var someBoard = new GameBoard
            {
                Board = new List<BoardCell>
                {
                    new BoardCell(0, 0, true),
                    new BoardCell(1, 0, true),

                    new BoardCell(0, 1, false),
                    new BoardCell(1, 1, false),

                    new BoardCell(0, 2, false),
                    new BoardCell(1, 2, true),
                }
            };

            await _sut.ComputeGeneration(someBoard);

            Assert.IsFalse(someBoard.Board.Single(c => c.XPosition == 1 && c.YPosition == 1).IsAlive);
        }
    }
}
