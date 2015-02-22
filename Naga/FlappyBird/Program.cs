using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{

    class Program
    {
        static public int points = 0;

        static void Main(string[] args)
        {
            Menu();
        }
        static void ReDrawObstacles(List<Obstacle> upObstacles, List<Obstacle> downObstacles, Boundaries b)
        {
            int downStartY = 0, upStartY = 0;


            for (int i = 0; i < downObstacles.Count; i++)
            {
                if (downObstacles[i].X <= b.leftX)
                {

                    downObstacles.Remove(downObstacles[i]);
                    points = Score(downObstacles);
                }
                if (downObstacles.Count == 0)
                {
                    break;
                }
                downStartY = downObstacles[i].Y;
                for (int j = 0; j < downObstacles[i].height; j++)
                {
                    if (downObstacles[i].X + 12 <= b.rightX)
                    {
                        if (j < 4)
                        {
                            PrintOnScreen(downObstacles[i].X, downObstacles[i].Y, downObstacles[i].upperPart[j], ConsoleColor.Red);

                        }
                        else
                            PrintOnScreen(downObstacles[i].X, downObstacles[i].Y, " |        |", ConsoleColor.Red);
                    }
                    downObstacles[i].Y++;

                }
                downObstacles[i].Y = downStartY;
                downObstacles[i].X -= 3;
            }
            int k = 0;
            for (int i = 0; i < upObstacles.Count; i++)
            {
                if (upObstacles[i].X <= b.leftX)
                {
                    upObstacles.Remove(upObstacles[i]);
                }
                if (upObstacles.Count == 0)
                {
                    break;
                }
                upStartY = upObstacles[i].Y;
                k = 0;
                for (int j = 0; j < upObstacles[i].height; j++)
                {
                    if (upObstacles[i].X + 12 <= b.rightX)
                    {
                        if (j < upObstacles[i].height - 4)
                        {
                            PrintOnScreen(upObstacles[i].X, upObstacles[i].Y, " |        |", ConsoleColor.Red);


                        }
                        else
                        {
                            PrintOnScreen(upObstacles[i].X, upObstacles[i].Y, upObstacles[i].upperPart[k], ConsoleColor.Red);
                            k++;
                        }
                    }
                    upObstacles[i].Y++;

                }
                upObstacles[i].Y = upStartY;
                upObstacles[i].X -= 3;
            }

        }
        static void PrintOnScreen(int x, int y, string str, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static string HighScore()
        {
            try
            {

                StreamReader scoreRead = new StreamReader(@"..\Score.txt");
                string highScore = scoreRead.ReadLine();
                scoreRead.Close();
                return highScore;
            }
            catch (FileNotFoundException e)
            {
                StreamWriter writer = new StreamWriter(@"..\Score.txt");
                writer.WriteLine("0");
                writer.Close();
                return "0";
            }



        }
        static public ConsoleKeyInfo Menu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine(@"
                                      #####    ##### ############ ######    #####  ####    ####
                                      ######  ###### ####         #######   #####  ####    ####
                                      ############## ###########  ##### ##  #####  ####    ####
                                      #####    ##### ###########  #####  ## #####  ####    #### 
                                      #####    ##### ####         #####   #######  ####    ####  
                                      #####    ##### ############ #####    ######    ######## ");


            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 7);
            Console.WriteLine("1. START GAME");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 6);
            Console.WriteLine("2. SCORE");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 5);
            Console.WriteLine("3. EXIT");
            var result = Console.ReadKey(true);
            return result;

        }//MENU - Emo - IN PROGRESS

        static void WriteScoreInFile()
        {

            StreamReader scoreRead = new StreamReader(@"..\Score.txt");

            string highScore = scoreRead.ReadLine();

            scoreRead.Close();
            if (highScore != null)
            {
                if (int.Parse(highScore) < points)
                {
                    var fs = new FileStream(@"..\Score.txt", FileMode.Truncate); // delete all text in the file
                    fs.Close();
                    StreamWriter file = new StreamWriter(@"..\Score.txt");
                    file.WriteLine(points.ToString());
                    file.Close();
                }
            }
            else
            {
                StreamWriter file = new StreamWriter(@"..\Score.txt");
                file.WriteLine(points.ToString());
                file.Close();

            }


        }


    }
}
