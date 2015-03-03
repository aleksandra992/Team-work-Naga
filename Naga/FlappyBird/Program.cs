using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace FlappyBird
{

    class Program
    {
        static public int points = 0;
        static public string hurtSound = @"..\..\hurt2.wav";
        static public string jumpSound = @"..\..\jump.wav";
        static public string pointSound = @"..\..\point.wav";
        static public string menuSound = @"..\..\menu.wav";

        static public Random p = new Random();

        static void Main()
        {



            bool startGame = false;
            try
            {
                Console.BufferWidth = Console.WindowWidth = 150;
                Console.BufferHeight = Console.WindowHeight = 41;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Please set your console font to 12 and Lucida Console font");
                return;
            }
            PlaySound(menuSound);
            SplashScreen.SplashScreenStart();
            ConsoleKeyInfo MenuKey;
            do
            {
                MenuKey = MenuScreen.Menu();
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





            Game();

           
        }
       
        private static void Game()
        {
            Bird b = new Bird();
            b.position.Y = Console.WindowHeight / 2;
            b.position.X = 15;

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
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (firstPressedKey.Key == ConsoleKey.UpArrow)
                    {
                        b.position.Y -= 2;
                        PlaySound(jumpSound);
                    }


                }

                b.position.Y++;
                ReDraw(b, upObstacles, downObstacles, bnd);
                if (b.position.Y <= 0 || b.position.Y + 4 >= Console.WindowHeight)
                {
                    //  points = Score(upObstacles);
                    PlaySound(hurtSound);
                    WriteScoreInFile();
                    PrintGameOver();
                    break;

                }

                bool hit = false;
                for (int i = 0; i < downObstacles.Count; i++)
                {
                    if (b.position.Y + b.bird1.Length - 1 >= downObstacles[i].Y && b.position.X + b.bird1.Length - 1 >= downObstacles[i].X)
                    {
                        //  points = Score(upObstacles);
                        hit = true;
                        PlaySound(hurtSound);
                        WriteScoreInFile();
                        PrintGameOver();
                        break;
                    }
                }
                if (hit)
                    break;
                for (int i = 0; i < upObstacles.Count; i++)
                {
                    if (b.position.Y + 1 <= upObstacles[i].Y + upObstacles[i].height && b.position.X + b.bird1.Length - 1 >= upObstacles[i].X)
                    {
                        // points = Score(upObstacles);
                        PlaySound(hurtSound);
                        WriteScoreInFile();
                        PrintGameOver();

                        hit = true;
                        break;
                    }
                }
                if (hit)
                    break;

                Thread.Sleep(100);
                Console.Clear();


            }
            while (true)
            {


                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).KeyChar;
                    if (key == 'y')
                    {
                        Game();
                        break;
                    }
                    else
                        if (key == 'n')
                            break;
                    Thread.Sleep(1000);
                }
            }
        }

        private static void AddUpDownObstacle(List<Obstacle> downObstacles, List<Obstacle> upObstacles, int numberOfObstacles)
        {
            int j = 0;

            for (int i = 1; i < numberOfObstacles; i++)
            {
                if (i > 0 && i < numberOfObstacles/3)
                {
                    j = 14;
                    AddObstacles(downObstacles, upObstacles, i, j);
                }
                else if (i >= numberOfObstacles / 3 && i < numberOfObstacles*2 / 3)
                {
                    j = 10;
                    AddObstacles(downObstacles, upObstacles, i, j);
                }
                else if (i >= numberOfObstacles * 2 / 3 && i < numberOfObstacles)
                {
                    j = 6;
                    AddObstacles(downObstacles, upObstacles, i, j);
                }
                else
                {
                    j = 5;
                    AddObstacles(downObstacles, upObstacles, i, j);
                }

            }




        }
        static public void PlaySound(string s)
        {
            var startScreen = new System.Media.SoundPlayer(s);
            startScreen.Play();


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
        {
            int pp = p.Next(1, 19);
            downObstacles.Add(new Obstacle(4 + pp, (i * 40) + i, Console.WindowHeight - (4 + pp)));
            upObstacles.Add(new Obstacle(41 - 4 - pp - j, (i * 40) + i, 0));
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
                            PrintOnScreen(downObstacles[i].X, downObstacles[i].Y, downObstacles[i].downPart[j], ConsoleColor.Green);

                        }
                        else
                            PrintOnScreen(downObstacles[i].X, downObstacles[i].Y, "  ║      ║", ConsoleColor.Green);
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
                            PrintOnScreen(upObstacles[i].X, upObstacles[i].Y, "  ║      ║", ConsoleColor.Green);


                        }
                        else
                        {
                            PrintOnScreen(upObstacles[i].X, upObstacles[i].Y, upObstacles[i].upperPart[k], ConsoleColor.Green);
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
        static int Score(List<Obstacle> obstacles)
        {
            int result = (50 - obstacles.Count) *2;
            PlaySound(pointSound);
            return result;
        }
        static string HighScore()
        {
            try
            {

                StreamReader scoreRead = new StreamReader(@"..\..\Score.txt");
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
        //MENU - Emo - IN PROGRESS
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

            StreamReader scoreRead = new StreamReader(@"..\..\Score.txt");

            string highScore = scoreRead.ReadLine();

            scoreRead.Close();
            if (highScore != null)
            {
                if (int.Parse(highScore) < points)
                {
                    var fs = new FileStream(@"..\..\Score.txt", FileMode.Truncate); // delete all text in the file
                    fs.Close();
                    StreamWriter file = new StreamWriter(@"..\..\Score.txt");
                    file.WriteLine(points.ToString());
                    file.Close();
                }
            }
            else
            {
                StreamWriter file = new StreamWriter(@"..\..\Score.txt");
                file.WriteLine(points.ToString());
                file.Close();

            }


        }
        static void PrintGameOver()
        {

            PrintOnScreen(Console.BufferWidth - 35, 15, "Game over, you've got " + points + " points", ConsoleColor.Yellow);
            PrintOnScreen(Console.BufferWidth - 35, 16, "Restart the game Y/N", ConsoleColor.Yellow);
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            string a = "Game over, you've got " + points + " points";
            synth.Speak(string.Format(a));


        }
        static void ReDraw(Bird b, List<Obstacle> upObstacles, List<Obstacle> downObstacles, Boundaries bnd)
        {
            ReDrawBird(b);
            ReDrawObstacles(upObstacles, downObstacles, bnd);
            ReDrawBoundaries(bnd);
            ReDrawScore(points);
        }
        static void ReDrawBird(Bird b)
        {
            int birdStartY = 0;
            birdStartY = b.position.Y;

            Random flap = new Random();
            int n = flap.Next(0, 2);
            if (n == 0)
            {
                for (int i = 0; i < b.bird1.Length; i++)
                {
                    PrintOnScreen(b.position.X, b.position.Y, b.bird1[i], ConsoleColor.Yellow);
                    b.position.Y++;
                }
                b.position.Y = birdStartY;
            }
            else
            {
                for (int i = 0; i < b.bird2.Length; i++)
                {
                    PrintOnScreen(b.position.X, b.position.Y, b.bird2[i], ConsoleColor.Yellow);
                    b.position.Y++;
                }
                b.position.Y = birdStartY;
            }
        }
        static void ReDrawScore(int score)
        {
            Point scorePosition = new Point(Console.BufferWidth - 20, 10);
            PrintOnScreen(scorePosition.X, scorePosition.Y, "SCORE: " + score.ToString(), ConsoleColor.Yellow);
            PrintOnScreen(scorePosition.X, scorePosition.Y - 2, "HIGH SCORE: " + HighScore(), ConsoleColor.Yellow);

        }
    }
}
