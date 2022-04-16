using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp25
{

    public abstract class Connect4
    {
        public char[,] b = new char[9, 10];
        public int dchoice, win, full, again;
        public string playername1, playername2;
        public char playerId1, playerId2;

        public Connect4(string name1, string name2, char Id1, char Id2)
        {
            full = win = again = 0;
            playername1 = name1;
            playername1 = name2;
            playerId1 = Id1;
            playerId2 = Id2;
        }
        public virtual int Dropping(char[,] board, string playingPlayer)
        {
            int dChoice;

            Console.WriteLine(playingPlayer + "Its your  Turn ");
            do
            {
                Console.WriteLine("Please enter any number between 1 and 7: ");
                dChoice = Convert.ToInt32(Console.ReadLine());
            } while (dChoice < 1 || dChoice > 7);

            while (board[1, dChoice] == 'X' || board[1, dChoice] == 'O')
            {
                Console.WriteLine("That row is full, please drop into another row: ");
                dChoice = Convert.ToInt32(Console.ReadLine());
            }

            return dChoice;
        }
        static void CheckDrop(char[,] board, string playingPlayer, int dChoice, string name1, string name2)
        {
            int length, turn;
            length = 6;
            turn = 0;

            do
            {
                if (board[length, dChoice] != 'X' && board[length, dChoice] != 'O')
                {
                    if (playingPlayer.Equals(name1))
                    {
                        board[length, dChoice] = 'X';
                        turn = 1;
                    }
                    else
                    {
                        board[length, dChoice] = 'O';
                        turn = 1;
                    }
                }
                else
                    length--;
            } while (turn != 1);
        }
        static void Display(char[,] board)
        {
            int Rows = 6, Columns = 7, i, j;

            for (i = 1; i <= Rows; i++)
            {
                Console.Write("|");
                for (j = 1; j <= Columns; j++)
                {
                    if (board[i, j] != 'X' && board[i, j] != 'O')
                        board[i, j] = '#';

                    Console.Write(board[i, j]);
                }
                Console.Write("| \n");
            }
        }
        static int CheckFullBoard(char[,] board)
        {
            int full = 0;
            for (int i = 1; i <= 7; i++)
            {
                if (board[1, i] != '*')
                    full++;
            }

            return full;
        }
        static int restart(char[,] board)
        {
            char r;

            Console.WriteLine("Would you like to replay the game? Yes('Y') No('N'): ");
            r = Console.ReadLine()[0];
            if (r == 'Y' || r == 'y')
            {
                for (int i = 1; i <= 6; i++)
                {
                    for (int j = 1; j <= 7; j++)
                    {
                        board[i, j] = '*';
                    }
                }
                return 1;
            }
            else
            {
                Console.WriteLine("Thanks for playing Connect 4! See you next time.");
                return 2;
            }
        }
        static int CheckFour(char[,] board, string playingPlayer, string name1, string name2)
        {
            char XO;
            int win;
            if (playingPlayer.Equals(name1))

                XO = 'X';

            else

                XO = 'O';

            win = 0;

            for (int i = 8; i >= 1; --i)
            {

                for (int j = 9; j >= 1; j--)
                {

                    if (board[i, j] == XO &&
                        board[i - 1, j - 1] == XO &&
                        board[i - 2, j - 2] == XO &&
                        board[i - 3, j - 3] == XO)
                    {
                        win = 1;
                    }


                    if (board[i, j] == XO &&
                        board[i, j - 1] == XO &&
                        board[i, j - 2] == XO &&
                        board[i, j - 3] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i - 1, j] == XO &&
                        board[i - 2, j] == XO &&
                        board[i - 3, j] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i - 1, j + 1] == XO &&
                        board[i - 2, j + 2] == XO &&
                        board[i - 3, j + 3] == XO)
                    {
                        win = 1;
                    }

                    if (board[i, j] == XO &&
                         board[i, j + 1] == XO &&
                         board[i, j + 2] == XO &&
                         board[i, j + 3] == XO)
                    {
                        win = 1;
                    }
                }

            }

            return win;
        }



        public virtual string GetPlayerName(string playingPlayer)
        {
            return playingPlayer;

        }
        public virtual void Play(string player1, string player2)
        {
            Display(b);
            int dChoice;
            do
            {
                dChoice = Dropping(b, player1);
                CheckDrop(b, player1, dChoice, player1, player2);
                Display(b);
                win = CheckFour(b, player1, player1, player2);
                if (win == 1)
                {
                    GetPlayerName(player1);
                    again = restart(b);
                    if (again == 2)
                    {
                        break;
                    }
                }

                dChoice = Dropping(b, player2);
                CheckDrop(b, player2, dChoice, player1, player2);
                Display(b);
                win = CheckFour(b, player2, player1, player2);
                if (win == 1)
                {
                    GetPlayerName(player2);
                    again = restart(b);
                    if (again == 2)
                    {
                        break;
                    }
                }
                full = CheckFullBoard(b);
                if (full == 7)
                {
                    Console.WriteLine("Oops! The board is full, so it is a draw!");
                    again = restart(b);
                }

            } while (again != 2);
        }

    }
    public class HumanvsHuman : Connect4
    {

        public HumanvsHuman(string name1, string name2, char Id1, char Id2) : base(name1, name2, Id1, Id2)
        { }
        public override void Play(string player1, string player2)
        {
            base.Play(player1, player2);
        }
        public override string GetPlayerName(string playingPlayer)
        {
            if (win == 1)
            {
                return playingPlayer + " Congratulations! You have Won!";
            }

            return base.GetPlayerName(playingPlayer);
        }


        internal class Program
        {
            static void Main(string[] args)
            {

                Console.WriteLine("Lets play Connect Four game");
                Console.WriteLine("Player One please enter your name: ");
                string playerName1 = Console.ReadLine();
                char player1ID = 'X';
                Console.WriteLine("Player Two please enter your name: ");
                string playerName2 = Console.ReadLine();
                char player2ID = 'O';
                Connect4 play = null;
                play = new HumanvsHuman(playerName1, playerName2, player1ID, player2ID);
                play.Play(playerName1, playerName2);


            }
        }
    }
}
