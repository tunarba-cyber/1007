using System;
using System.Threading;

namespace _1007MiniProject.Persistance.UI
{
    public static class ServiceUI
    {
        private static readonly Random _rng = new Random();

        private static readonly string[] LoadingFlavor =
        {
            "Recalling to base",
            "Warding the river bush",
            "Clearing the jungle camp",
            "Buying items back at the shop",
            "Pinging the Rift Herald",
            "Building your Mythic item",
            "Checking Baron timer",
            "Respawning at the Nexus"
        };

        public static void TypeWriter(string text, int delayMs = 12)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        public static void Loading(string label = null, int dots = 4, int stepDelayMs = 180)
        {
            label ??= LoadingFlavor[_rng.Next(LoadingFlavor.Length)];
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(label);
            for (int i = 0; i < dots; i++)
            {
                Thread.Sleep(stepDelayMs);
                Console.Write(".");
            }
            Thread.Sleep(stepDelayMs);
            Console.WriteLine(" done.");
            Console.ResetColor();
        }

        public static void Header(string title, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            string bar = new string('=', Math.Max(title.Length + 8, 30));
            Console.WriteLine(bar);
            Console.WriteLine($"   ⚔  {title}");
            Console.WriteLine(bar);
            Console.ResetColor();
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TypeWriter($"🏆 PENTAKILL! {message}");
            Console.ResetColor();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            TypeWriter($"☠ You have been slain: {message}");
            Console.ResetColor();
        }

        public static void Empty(string entityNamePlural)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            TypeWriter($"🌫 The Fog of War hides all {entityNamePlural}... nothing found on the Rift.");
            Console.ResetColor();
        }

        public static void Pause(string message = "Press any key to recall back to the Nexus (menu)...")
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}