using System;

namespace alg {
    public static class Utils {

        static ConsoleColor savedColor;

        public static void saveConsoleColor() {
            savedColor = Console.ForegroundColor;
        }

        public static void restoreConsoleColor() {
            Console.ForegroundColor = savedColor;
        }

        public static void Error(string sys, string data) {
            saveConsoleColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{sys}]: {data}");

            restoreConsoleColor();

            System.Environment.Exit(1);
        }

        public static void Warn(string sys, string data) {
            saveConsoleColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{sys}]: {data}");

            restoreConsoleColor();
        }

        public static void Info(string sys, string data) {
            saveConsoleColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[{sys}]: {data}");

            restoreConsoleColor();
        }
    }
}