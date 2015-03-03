using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class SplashScreen // Boris
    {
       public static ConsoleKeyInfo SplashScreenStart()
        {
            
            int horizontalAllign = Console.WindowWidth / 2;
            int verticalAllign = Console.WindowHeight / 2 - 8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.Write("{0,118}", "███████╗██╗      █████╗ ██████╗ ██████╗ ██╗   ██╗    ██████╗ ██╗██████╗ ██████╗ \n");
            Console.Write("{0,118}", "██╔════╝██║     ██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝    ██╔══██╗██║██╔══██╗██╔══██╗\n");
            Console.Write("{0,118}", "█████╗  ██║     ███████║██████╔╝██████╔╝ ╚████╔╝     ██████╔╝██║██████╔╝██║  ██║\n");
            Console.Write("{0,118}", "██╔══╝  ██║     ██╔══██║██╔═══╝ ██╔═══╝   ╚██╔╝      ██╔══██╗██║██╔══██╗██║  ██║\n");
            Console.Write("{0,118}", "██║     ███████╗██║  ██║██║     ██║        ██║       ██████╔╝██║██║  ██║██████╔╝\n");
            Console.Write("{0,118}", "╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝        ╚═╝       ╚═════╝ ╚═╝╚═╝  ╚═╝╚═════╝ \n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Threading.Thread.Sleep(1200);
            Console.SetCursorPosition(horizontalAllign - 15, verticalAllign + 15);
            Console.WriteLine("P R E S S  K E Y  T O  S T A R T");
            Console.ResetColor();
            var key = Console.ReadKey(true);

            return key;

        }
    }
}
