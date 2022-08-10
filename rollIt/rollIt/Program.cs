// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Numerics;



namespace rollIt
{
    public class Program
    {
        static List<int> savePositionsList = new List<int>();
        public static Vector2[] directions = new Vector2[8]
        {
            new Vector2(-1,0),
            new Vector2(-1,1),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(1,-1),
            new Vector2(0,-1),
            new Vector2(-1,-1)

        };

        static int[,] bordGame = new int[,] {
                { 0, 2, 2, 1, 1, 1, 1, 2 },
                { 4, 1, 2, 1, 3, 2, 1, 1 },
                { 3, 2, 2, 3, 2, 1, 1, 3 },
                { 3, 3, 4, 3, 2, 2, 2, 2 },
                { 1, 4, 2, 3, 2, 3, 1, 1 },
                { 4, 1, 1, 3, 1, 1, 2, 2 },
                { 1, 2, 3, 4, 1, 1, 3, 4 },
                { 5, 2, 3, 2, 3, 1, 2, 4 },
            };


        static void PrintBoard(int[,] bordGame)
        {
            for (int x = 0; x < bordGame.GetLength(0); x += 1)
            {
                for (int y = 0; y < bordGame.GetLength(1); y += 1)
                {
                    Console.Write(bordGame[x, y] + " ");
                }
                Console.WriteLine();
            }
        }

        static bool IsWithinBounds(int x, int y)
        {
            if (x > 7 || x < 0 || y > 7 || y < 0)
            {
                return false;
            }

            return true;
        }

        static bool HasNeighbour(int x, int y)
        {
            foreach (var direction in directions)
            {

                if (IsWithinBounds(x + direction.X, y + direction.Y))
                {
                    if (bordGame[x + direction.X, y + direction.Y] != 0)
                    {
                        return true;
                    }
                }

            }

            return false;

        } 

        static void FindTheSameNumber(int[,] bordGame, int posX, int posY, int playerNumber)
        {
            foreach (var direction in directions)
            {
                Vector2 newVec = new Vector2(direction.X + posX, direction.Y + posY);
                if(IsWithinBounds(newVec.X, newVec.Y) && bordGame[newVec.X, newVec.Y]!=0 && bordGame[newVec.X, newVec.Y] != playerNumber)
                {
                    CheckAllDirection(newVec.X, newVec.Y, direction.X, direction.Y, playerNumber);
                }

            }

        }

        static void CheckAllDirection(int posX, int posY,int dirX, int dirY, int playerNumber)
        {
            if (IsWithinBounds(posX, posY) && bordGame[posX, posY] != 0 && bordGame[posX, posY] != playerNumber)
            {
                savePositionsList.Add(posX);
                savePositionsList.Add(posY);
                CheckAllDirection(posX+dirX, posY+dirY, dirX, dirY, playerNumber);
            }
            else if(IsWithinBounds(posX, posY) && bordGame[posX, posY] == playerNumber)
            {
                for (int i = savePositionsList.Count; i > 0; i-=2)
                {
                    bordGame[savePositionsList[0], savePositionsList[1]] = playerNumber;
                    savePositionsList.RemoveAt(0);
                    savePositionsList.RemoveAt(0);
                }

            }
            else
            {
                savePositionsList.Clear();
            }
            
        }

        static void StartGame(int[,] array2D, int playerNumber)
        {
            Console.WriteLine();
            Console.WriteLine("Bord: ");

            PrintBoard(array2D);

            Console.WriteLine();


            Console.WriteLine("Player {0} write position: ", playerNumber);

            string[] arrPosit = Console.ReadLine().Split(' ');
            if(arrPosit.Length > 2 )
            {
                Console.WriteLine("Write posistion igen!");
                StartGame(array2D, playerNumber);
            }

            try
            {
                int posX = int.Parse(arrPosit[0]);
                int posY = int.Parse(arrPosit[1]);

                if (HasNeighbour(posX, posY))
                {

                    if (array2D[posX, posY] == 0)
                    {
                        array2D[posX, posY] = playerNumber;
                        Console.WriteLine("Result: ");
                        PrintBoard(bordGame);
                        FindTheSameNumber(bordGame, posX, posY, playerNumber);
                    }
                    else
                    {
                        Console.WriteLine("Сhoose another position, this one is already taken");
                        StartGame(bordGame, playerNumber);
                    }


                }
                else
                {
                    Console.WriteLine("Choose, another position");
                    StartGame(bordGame, playerNumber);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("You may write just number, like this : 3 0");
                StartGame(bordGame, playerNumber);
            }


        }


        static void CheckWinner(int[,]bordGame)
        {
            int player1 = 0;
            int player2 = 0;
            int player3 = 0;
            int player4 = 0;

            for (int x = 0; x < bordGame.GetLength(0); x += 1)
            {
                for (int y = 0; y < bordGame.GetLength(1); y += 1)
                {
                    switch (bordGame[x,y])
                    {
                        case 1:
                            player1++;
                            break;
                        case 2:
                            player2++;
                            break;
                        case 3:
                            player3++;
                            break;
                        case 4:
                            player4++;
                            break;
                        default:
                            break;
                    }
                }
            }
            
            Console.WriteLine("Result: ");
            Console.WriteLine("Player 1 {0}: ", player1);
            Console.WriteLine("Player 2 {0}: ", player2);
            Console.WriteLine("Player 3 {0}: ", player3);
            Console.WriteLine("Player 4 {0}: ", player4);

            Console.WriteLine();

            if (player1 > player2 && player1 > player3 && player1 > player4) { Console.WriteLine("Winner er: {0}", player1); return; }
            if (player2 > player1 && player2 > player3 && player2 > player4) { Console.WriteLine("Winner er: {0}", player2); return; }
            if (player3 > player2 && player3 > player1 && player1 > player4) { Console.WriteLine("Winner er: {0}", player3); return; }
            Console.WriteLine("Winner er: {0}", player4);
        }

        static void Main(string[] args)
        {

            int playerNumber = 1;

            for (int i = 0; i < 1; i++)
            {
                if (playerNumber > 4)
                {
                    playerNumber = 1;
                }
                StartGame(bordGame, playerNumber);
                playerNumber++;

            }

            CheckWinner(bordGame);

        }

    }
}




