using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTakToe3
{
    [TestClass]
    [UseReporter(typeof(BeyondCompareReporter))]
    public class TicTakToe
    {

        [TestMethod]
        public void TestEmptyboard()
        {
            var board = new Board();
            Approvals.Verify(board);
        }


        [TestMethod]
        public void TestAllCombinationsForAllLocationsAndMarkerXAndO()
        {
            var listOfMarker = new List<string>() { Board.MarkerX, Board.MarkerO};
            var listOfLocations = new List<string>()
            {
                Board.TopLeft,
                Board.TopMiddle,
                Board.TopRight,

                Board.MiddleLeft,
                Board.MiddleCenter,
                Board.MiddleRight,

                Board.BottomLeft,
                Board.BottomMiddle,
                Board.BottomRight

            };

            CombinationApprovals.VerifyAllCombinations((x, i) =>
            {
                var board = new Board();
                board.Place(x,i);
                return Environment.NewLine + board.ToString();
            },listOfMarker,listOfLocations);

        }

        [TestMethod]
        public void TestDiagonalWinnerX()
        {
            WinnerMethod(Board.MarkerX, Board.TopLeft, Board.MiddleCenter, Board.BottomRight);
        }


        [TestMethod]
        public void TestTopRowIsWinner()
        {
            WinnerMethod(Board.MarkerX, Board.TopLeft, Board.TopMiddle, Board.TopRight);
        }

        [TestMethod]
        public void TestEmptyBoardHasNoWinner()
        {
            var board = new Board();
            Assert.IsFalse(board.IsWinner());
        }

        [TestMethod]
        public void TestWinnerBottomRowO()
        {
            WinnerMethod(Board.MarkerO, Board.BottomLeft, Board.BottomMiddle, Board.BottomRight);
        }

        [TestMethod]
        public void TestAllWinningConditionsForMarkerXAndO()
        {
            var listOfWinnerTopRow = new List<string>()
            {
                Board.TopLeft,
                Board.TopMiddle,
                Board.TopRight
            };

            var  listOfWinningLeftDiagnoal = new List<string>()
            {
                Board.TopLeft,
                Board.MiddleCenter,
                Board.BottomRight
            };

            var listOfWinningBottomRow = new List<string>()
            {
                Board.BottomLeft,
                Board.BottomMiddle,
                Board.BottomRight
            };

            var listOfWinningRightDiagnoal = new List<string>()
            {
                Board.TopRight,
                Board.MiddleCenter,
                Board.BottomLeft
            };

            var listOfWinningMiddleRow = new List<string>()
            {
                Board.MiddleLeft,
                Board.MiddleCenter,
                Board.MiddleRight
            };

            var listOfWinningLeftRow = new List<string>()
            {
                Board.TopLeft,
                Board.MiddleLeft,
                Board.BottomLeft
            };

            var listOfWinnerMiddleRow = new List<string>()
            {
                Board.TopMiddle,
                Board.MiddleCenter,
                Board.BottomMiddle
            };

            var listOfWinnerRightRow = new List<string>()
            {
                Board.TopRight,
                Board.MiddleRight,
                Board.BottomRight
            };

            var listOfListForAllWinners = new List<Scenario>()
            {
                
                new Scenario {Title = "Top Row", Moves = listOfWinnerTopRow},
                new Scenario {Title = "Left Diagonal", Moves = listOfWinningLeftDiagnoal },
                new Scenario {Title = "Bottom Row ", Moves = listOfWinningBottomRow},
                new Scenario {Title = "Right Diagonal ",Moves = listOfWinningRightDiagnoal},
                new Scenario {Title = "Middle Row", Moves = listOfWinningMiddleRow},
                new Scenario {Title ="Left column", Moves = listOfWinningLeftRow},
                new Scenario {Title = "Middle column", Moves = listOfWinnerMiddleRow},
                new Scenario {Title = "Right column",Moves = listOfWinnerRightRow}
               
            };
            var listOfMarkers = new List<string>() {Board.MarkerX,Board.MarkerO};


            CombinationApprovals.VerifyAllCombinations((x, WinnerLocations ) =>
            {
                var board = new Board();
                foreach (var i in WinnerLocations.Moves)
                {
                    board.Place(x,i);
                }
                return Environment.NewLine + board.ToString() + $"\n {x} is the winner of :{WinnerLocations.Title}";
            },listOfMarkers,listOfListForAllWinners);

        }

        [TestMethod]
        public void TestWhenTheGameHasWinnerAndLoser()
        {
            var listOfWinningRightDiagnoal = new List<string>()
            {
                Board.TopRight,
                Board.MiddleCenter,
                Board.BottomLeft
            };

            var listOfWinningMiddleRow = new List<string>()
            {
                Board.MiddleLeft,
                Board.MiddleCenter,
                Board.MiddleRight
            };
            
            var noneCompleted = new List<string>()
            {
                Board.TopLeft,
                Board.MiddleLeft,
            };

            var listOfLocations = new List<Scenario>()
            {
                new Scenario {Title = "Winner of right Diagolan", Moves = listOfWinningRightDiagnoal},
                new Scenario{Title = "Winner of middle Row ",Moves = listOfWinningMiddleRow},
                new Scenario {Title = "None compeleted ",Moves = noneCompleted}
            };
            var listOfMarkers = new List<string>() { Board.MarkerX, Board.MarkerO };

            CombinationApprovals.VerifyAllCombinations((x, Game) =>
            {
                var board = new Board();
                foreach (var i in Game.Moves)
                {
                    board.Place(x, i);
                }

                return Environment.NewLine + $" {x} is {Game.Title}" + "\n\n " +board.ToString();

            },listOfMarkers,listOfLocations);


        }

        private static void WinnerMethod(string markerX, string left, string center, string right)
        {
            var board = new Board();
            board.Place(markerX, left);
            board.Place(markerX, center);
            board.Place(markerX, right);
            Assert.IsTrue(board.IsWinner());
            Approvals.Verify(board);
        }

    }

    public class Scenario
    {
        public string Title = " ";
        public List<string> Moves { get; set; }
    }
}
