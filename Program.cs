using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToe Game = new TicTacToe();
            Game.PlayGame();
        }
    }

    class TicTacToe
    {
        string[,] GameMap = new string[3, 3];
        bool IsGameWon { get; set; }
        int TotalTurns { get; set; }
        int PlayerUp { get; set; }
        int NumberOfTurns { get; set; }
        List<int> MovesMade { get; set; }

        public TicTacToe()
        {
            ResetGame();
        }

        //public variables
        public void PlayGame()
        {
            do
            {
                DrawMap();
                PlayerTurn();
                DrawMap();
                EndPlayerTurn();
            } while (!IsGameWon);
        }

        private void Intro()
        {
            Console.WriteLine("\n=============================================================");
            Console.WriteLine("Welcome to Tic Tac Toe. A game coded in C# by Mark A. Ruzicka");
            Console.WriteLine("  Simply type the number of the box you wish to tic or tac.  ");
            Console.WriteLine("=============================================================\n");
        }
        private void ResetGame()
        {
            GameMap = new string[3,3]
            {
                {"1","2","3"},
                {"4","5","6"},
                {"7","8","9"}
            };
            IsGameWon = false;
            PlayerUp = 1;
            NumberOfTurns = 1;
            MovesMade = new List<int> { };
        }
        private void DrawMap()
        {
            Console.Clear();
            Intro();
            Console.WriteLine("   -------------------");
            Console.WriteLine("   |  {0}  |  {1}  |  {2}  |", GameMap[0,0], GameMap[0, 1], GameMap[0, 2]);
            Console.WriteLine("   -------------------");
            Console.WriteLine("   |  {0}  |  {1}  |  {2}  |", GameMap[1,0], GameMap[1, 1], GameMap[1, 2]);
            Console.WriteLine("   -------------------");
            Console.WriteLine("   |  {0}  |  {1}  |  {2}  |", GameMap[2,0], GameMap[2, 1], GameMap[2, 2]);
            Console.WriteLine("   -------------------");
        }

        private void SetPlayerUp()
        {
            if (PlayerUp == 1)
            {
                PlayerUp = 2;
            }
            else
            {
                PlayerUp = 1;
            }
        }

        bool ValidMove(string move)
        {
            int moveN;
            bool success = int.TryParse(move, out moveN);

            if (success)
            {
                if ((!MovesMade.Contains(moveN)) && (moveN >= 1 && moveN <= 9))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid move!");
                }
            }
            return false;

        }

        private void PlayerTurn()
        {
            string move = "";
            do
            {
                Console.Write("\nPlayer {0} turn: ", PlayerUp);
                move = Console.ReadLine();
            } while (!ValidMove(move));

            UpdateMap(move, PlayerUp);

            if (PlayerUp == 2)
            {
                NumberOfTurns++;
            }
        }

        private void EndPlayerTurn()
        {
            CheckForWinCondition();
            if (IsGameWon)
            {
                EndGame();
            }
            else
            {
                SetPlayerUp();
            }

        }

        private void UpdateMap(string move, int player)
        {
            string xo;
            if (player == 1)
            {
                xo = "X";
            }
            else
            {
                xo = "O";
            }

            int grid;
            bool success = int.TryParse(move, out grid);

            if (success)
            {
                MovesMade.Add(grid);

                switch (grid)
                {
                    case 1:
                        GameMap[0, 0] = xo;
                        break;
                    case 2:
                        GameMap[0, 1] = xo;
                        break;
                    case 3:
                        GameMap[0, 2] = xo;
                        break;
                    case 4:
                        GameMap[1, 0] = xo;
                        break;
                    case 5:
                        GameMap[1, 1] = xo;
                        break;
                    case 6:
                        GameMap[1, 2] = xo;
                        break;
                    case 7:
                        GameMap[2, 0] = xo;
                        break;
                    case 8:
                        GameMap[2, 1] = xo;
                        break;
                    case 9:
                        GameMap[2, 2] = xo;
                        break;
                }
            }
        }

        void CheckForWinCondition()
        {
            string[] colValues = new string[3];
            string[] rowValues = new string[3];

            // check for a win condition by columns
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    colValues[row] = GameMap[row, col];
                }
                if ((colValues[0] == colValues[1]) && (colValues[1] == colValues[2]))
                {
                    IsGameWon = true;
                }
            }

            // check for a win condition by rows
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    rowValues[col] = GameMap[row, col];
                }
                if ((rowValues[0] == rowValues[1]) && (rowValues[1] == rowValues[2]))
                {
                    IsGameWon = true;
                }
            }

            // check diagonal win condition
            if ((GameMap[0, 0] == GameMap[1, 1]) && (GameMap[1, 1] == GameMap[2, 2]))
            {
                IsGameWon = true;
            }
            // check diagonal win condition
            if ((GameMap[2, 0] == GameMap[1, 1]) && (GameMap[1, 1] == GameMap[0, 2]))
            {
                IsGameWon = true;
            }
        }

        void EndGame()
        {
            Console.WriteLine("Player {0} wins on turn {1}!", PlayerUp, NumberOfTurns);
            Console.Write("Do you want to play again? (y/n) ");
            char answer = Console.ReadLine()[0];
            if ((answer == 'y') || (answer == 'Y'))
            {
                ResetGame();
            }
        }
    }
}
