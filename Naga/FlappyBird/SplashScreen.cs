using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class SplashScreen
    {
       public static ConsoleKeyInfo SplashScreenStart()
        {
            string logo = @"
######## ####                                                                          ####
######## ####                                             ##########                   ####
####     ####                                             ####   #### #### #######     ####
######## ####  ######### #########  #########  ####  #### ####   #### #### ####### ########
######## #### ####  #### ###   #### ###   #### ####  #### ##########       ####  ##########
####     #### ####  #### ###    ### ###    ### ####  #### ####   #### #### ####  ####   ###
####     #### ####  #### ###   #### ###   ####  ######### ####   #### #### ####  ##########
####     ####  ######### #########  #########       ##### ##########  #### ####   #########
                         ####       ####        ########                               
                         ####       ####        ######  
";

            int horizontalAllign = Console.WindowWidth / 2;
            int verticalAllign = Console.WindowHeight / 2 - 8;
            Console.SetCursorPosition(horizontalAllign, verticalAllign);

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(logo);

            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Threading.Thread.Sleep(1200);
            Console.SetCursorPosition(horizontalAllign - 15, verticalAllign + 15);
            Console.WriteLine("P R E S S    S T A R T");
            Console.ResetColor();

            var key = Console.ReadKey(true);
            return key;

        }
    }
}
