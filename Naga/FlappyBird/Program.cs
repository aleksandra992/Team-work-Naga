using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlappyBird
{

    class Program
    {
        static public int points = 0;

        static void Main(string[] args)
        {
           SplashScreen.SplashScreenStart();
           ConsoleKeyInfo MenuKey;
           bool startGame = false;

           Console.BufferWidth = Console.WindowWidth = 150;
           Console.BufferHeight = Console.WindowHeight = 41;

           do
           {
               MenuKey = Menu();
               switch (MenuKey.KeyChar.ToString())
               {
                   case "1": startGame = true; break;
                   case "2": ScoreMenu(); break;
                   case "3": Environment.Exit(0); break;

               }
               if (startGame == true)
               {
                   break;
               }
           } while (true); // menu loop

            Bird b = new Bird();
            b.position.Y = Console.WindowHeight / 2;
            b.position.X = 15;
            string hurtSound = @"hurt.wav";
            string jumpSound = @"jump.wav";
            //down obstacles


            int numberOfObstacles = 51;
            List<Obstacle> downObstacles = new List<Obstacle>();
            List<Obstacle> upObstacles = new List<Obstacle>();
            AddUpDownObstacle(downObstacles, upObstacles, numberOfObstacles);
            Boundaries bnd = new Boundaries();
            bnd.height = Console.WindowHeight;
            bnd.leftX = 2;
            bnd.leftY = 0;
            bnd.rightX = Console.BufferWidth - 40;
            bnd.rightY = 0;

            while (true)
            {
                if (upObstacles.Count == 0 && downObstacles.Count == 0)
                {
                    PrintOnScreen(Console.BufferWidth - 35, 12, "Congratulations, you've got " + points, ConsoleColor.Yellow);
                    PrintOnScreen(Console.BufferWidth - 35, 14, "Restart the game Y/N", ConsoleColor.Yellow);
                    break;

                }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo firstPressedKey = Console.ReadKey(true);
                    if (firstPressedKey.Key == ConsoleKey.UpArrow)
                    {
                        b.position.Y -= 2;
                        //play jump sound
                    }


                }

                b.position.Y++;

               // ReDraw everything
                if (b.position.Y <= 0 || b.position.Y + 4 >= Console.WindowHeight)
                {
                   
                   // play hurt sound 
                    WriteScoreInFile();
                 //print game over
                    break;

                }

                bool hit = false;
                for (int i = 0; i < downObstacles.Count; i++)
                {
                    if (b.position.Y + b.bird.Length - 1 >= downObstacles[i].Y && b.position.X + b.bird.Length - 1 >= downObstacles[i].X)
                    {
                      //play hurt sound
                        WriteScoreInFile();
                     //print game over
                        hit = true;
                        break;
                    }
                }
                if (hit)
                    break;
                for (int i = 0; i < upObstacles.Count; i++)
                {
                    if (b.position.Y + 1 <= upObstacles[i].Y + upObstacles[i].height && b.position.X + b.bird.Length - 1 >= upObstacles[i].X)
                    {
                       
                      //play hurt sound
                        WriteScoreInFile();
                      //print game over

                        hit = true;
                        break;
                    }
                }
                if (hit)
                    break;

                Thread.Sleep(100);
                Console.Clear();


            }
        }

        private static void AddUpDownObstacle(List<Obstacle> downObstacles, List<Obstacle> upObstacles, int numberOfObstacles)
        {
            int j = 4;
            int pom = 0;
            int pom1 = 0;
            for (int i = 1; i < numberOfObstacles; i++)
            {
                if (i < 11)
                {
                    AddObstacles(downObstacles, upObstacles, i, j);
                    j++;
                }
                else
                    if (i >= 11 && i < 26)
                    {

                        AddObstacles(downObstacles, upObstacles, i, j);
                        pom = j + 1;


                    }
                    else
                        if (i >= 26 && i < 41)
                        {
                            AddObstacles(downObstacles, upObstacles, i, pom);


                            pom1 = pom + 1;

                        }
                        else
                        {
                            AddObstacles(downObstacles, upObstacles, i, pom1);
                        }




            }
        }
        static void ReDrawBoundaries(Boundaries b)
        {
            int startLeftY = b.leftY;
            int startRightY = b.rightY;
            for (int i = 0; i < b.height; i++)
            {

                PrintOnScreen(b.leftX, b.leftY, "|", ConsoleColor.Black);
                PrintOnScreen(b.rightX, b.rightY, "|", ConsoleColor.White);
                b.leftY++;
                b.rightY++;
            }
            b.rightY = startRightY;
            b.leftY = startLeftY;
        }
        static void AddObstacles(List<Obstacle> downObstacles, List<Obstacle> upObstacles, int i, int j)
        { }
        static void ReDrawObstacles(List<Obstacle> upObstacles, List<Obstacle> downObstacles, Boundaries b)
        {
            int downStartY = 0, upStartY = 0;


            for (int i = 0; i < downObstacles.Count; i++)
            {
                if (downObstacles[i].X <= b.leftX)
                {

                    downObstacles.Remove(downObstacles[i]);
                   // points = Score(downObstacles);
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
        static public void ScoreMenu()
        {
            HighScore();
            Console.Clear();
            //TODO: print SCORE title
            Console.WriteLine(@"               
                                  ###########    ###########    ###########    #########    ###########
                                  ###########    ###########    ###########    ####  ###    ####
                                  ####           ###            ##       ##    ####  ###    ####
                                  ####           ###            ##       ##    #########    ######
                                  ###########    ###            ##       ##    ########     #########
                                         ####    ###            ##       ##    ###  ###     ######
                                         ####    ###            ##       ##    ###   ###    ####
                                  ###########    ###########    ###########    ###    ###   ####
                                  ###########    ###########    ###########    ###     ###  ###########");
            StreamReader scoreReader = new StreamReader(@"..\Score.txt");
            string score = scoreReader.ReadLine();
            int[] scoreTab = new int[5];
            int counter = 0;

            while (score != null)
            {
                scoreTab[counter] = int.Parse(score);
                score = scoreReader.ReadLine();
                counter++;
            }
            scoreReader.Close();

            for (int i = 0; i < scoreTab.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 7 + i);
                Console.WriteLine("{0}.{1}", i + 1, scoreTab[i]);
            }

            ConsoleKeyInfo scoreKey;
            Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 2);
            Console.WriteLine("Pess ESC to go BACK.");

            do
            {
                scoreKey = Console.ReadKey(true);
            } while (scoreKey.Key != ConsoleKey.Escape);
        }//Emo

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
