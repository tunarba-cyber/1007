using _1007MiniProject.Persistance.UI;
using System;
using System.Threading;

namespace _1007MiniProject.Presentation.UI
{
    public static class MenuUI
    {
        public static void SplashScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            ServiceUI.TypeWriter("Welcome, Summoner...", 20);
            ServiceUI.Loading("Connecting to the Movie Rift", 5, 200);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  __  __  _____ __      __ _____ ______   _____  _____ ______ _______
 |  \/  |/ ____|\ \    / /|_   _|  ____| |  __ \|_   _|  ____|__   __|
 | \  / | |  __  \ \  / /   | | | |__    | |__) | | | | |__     | |
 | |\/| | | |_ |  \ \/ /    | | |  __|   |  _  /  | | |  __|    | |
 | |  | | |__| |   \  /    _| |_| |____  | | \ \ _| |_| |____   | |
 |_|  |_|\_____|    \/    |_____|______| |_|  \_\_____|______|  |_|
");
            Console.ResetColor();
            ServiceUI.Pause("Press any key to enter the Rift...");
        }

        public static void MainMenuHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=====================================================");
            Console.WriteLine("        MOVIE MANAGEMENT SYSTEM  —  THE RIFT");
            Console.WriteLine("=====================================================");
            Console.ResetColor();
        }

        public static void PrintMenuOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" [Top Lane]     1.  Create Genre");
            Console.WriteLine(" [Jungle]       2.  Create Actor");
            Console.WriteLine(" [Mid Lane]     3.  Create Movie");
            Console.WriteLine(" [Bot Lane]     4.  Show All Genres");
            Console.WriteLine(" [Support]      5.  Show All Actors");
            Console.WriteLine("                6.  Show All Movies");
            Console.WriteLine("                7.  Show Movie Details");
            Console.WriteLine("                8.  Assign Actor To Movie");
            Console.WriteLine("                9.  Show Movie Actors");
            Console.WriteLine("                10. Search Movie");
            Console.WriteLine("                11. Movie Statistics");
            Console.WriteLine("                12. Delete Movie (Soft Delete)");
            Console.WriteLine("                13. Restore Movie");
            Console.WriteLine("                0.  Surrender (Exit)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=====================================================");
            Console.ResetColor();
        }

        public static string PromptChoice()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Select your action, Summoner: ");
            Console.ForegroundColor = ConsoleColor.White;
            string choice = Console.ReadLine();
            Console.ResetColor();
            return choice;
        }

        public static void InvalidOption()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            ServiceUI.TypeWriter("⚠ Invalid command! You have been reported for feeding the console...");
            Console.ResetColor();
            ServiceUI.Pause();
        }

        public static void ExitAnimation()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            ServiceUI.Loading("Recalling to the Nexus", 5, 200);
            ServiceUI.TypeWriter("GG WP! Thanks for playing the Movie Rift. See you next game!", 15);
            Console.ResetColor();
            Thread.Sleep(500);
        }
    }
}