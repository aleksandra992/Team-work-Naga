using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class MenuScreen
    {
        static public ConsoleKeyInfo Menu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\n");
            Console.Write("{0,95}", "███╗   ███╗███████╗███╗   ██╗██╗   ██╗\n");
            Console.Write("{0,95}", "████╗ ████║██╔════╝████╗  ██║██║   ██║\n");
            Console.Write("{0,95}", "██╔████╔██║█████╗  ██╔██╗ ██║██║   ██║\n");
            Console.Write("{0,95}", "██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║   ██║\n");
            Console.Write("{0,95}", "██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝\n");
            Console.Write("{0,95}", "╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ \n");
            Console.ResetColor();

            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 7);
            Console.WriteLine("1. START GAME");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 6);
            Console.WriteLine("2. SCORE");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 5);
            Console.WriteLine("3. EXIT");
            var result = Console.ReadKey(true);
            return result;

        }
    }
}
