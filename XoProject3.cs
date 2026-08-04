using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoeproject3
{
    internal class Program
    {
        static string[] board = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static string currentplayer = "X";
        static void DisplayBoard()
        {
            Console.WriteLine();
            Console.WriteLine(board[0] + " | " + board[1] + " | " + board[2]);
            Console.WriteLine(board[3] + " | " + board[4] + " | " + board[5]);
            Console.WriteLine(board[6] + " | " + board[7] + " | " + board[8]);
            Console.WriteLine();
        }
        static void PlayerMove()
        {
            Console.WriteLine("player " + currentplayer + ", enter a position between 1 and 9: ");
            int position = int.Parse(Console.ReadLine());
            while (position < 1 || position > 9 || (board[position - 1] == "X" || board[position - 1] == "O"))
            {
                Console.WriteLine("Invalid move. enter another position (1-9): ");
                position = int.Parse(Console.ReadLine());
            }

            board[position - 1] = currentplayer;
        }
        static bool CheckWin()
        {
            if (board[0] == currentplayer && board[1] == currentplayer && board[2] == currentplayer)
                return true;

            if (board[3] == currentplayer && board[4] == currentplayer && board[5] == currentplayer)
                return true;

            if (board[6] == currentplayer && board[7] == currentplayer && board[8] == currentplayer)
                return true;

            if (board[0] == currentplayer && board[3] == currentplayer && board[6] == currentplayer)
                return true;

            if (board[1] == currentplayer && board[4] == currentplayer && board[7] == currentplayer)
                return true;

            if (board[2] == currentplayer && board[5] == currentplayer && board[8] == currentplayer)
                return true;

            if (board[0] == currentplayer && board[4] == currentplayer && board[8] == currentplayer)
                return true;

            if (board[2] == currentplayer && board[4] == currentplayer && board[6] == currentplayer)
                return true;

            return false;
        }

        static void ChangePlayer()
        {
            if (currentplayer == "X")
                currentplayer = "O";
            else
                currentplayer = "X";
        }
        static bool CheckDraw()
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != "X" && board[i] != "O")
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            bool gameover = false;

            while (!gameover)
            {
                DisplayBoard();

                PlayerMove();

                if (CheckWin())
                {
                    DisplayBoard();
                    Console.WriteLine("Player " + currentplayer + " wins");
                    gameover = true;
                }
                else if (CheckDraw())
                {
                    DisplayBoard();
                    Console.WriteLine("draw");
                    gameover = true;
                }
                else
                {
                    ChangePlayer();
                }
            }
        }
    }
}
