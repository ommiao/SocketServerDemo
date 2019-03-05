using System;

namespace SocketServerDemo.socket
{
    public class Logger
    {

        public static void ShowCurrentUser()
        {
            Console.SetWindowSize(130, 32);
            PrintMessageTitle("This is a test title.", 3);
        }

        private static void PrintMessageTitle(string title, int contentColNumber)
        {
            Console.Write("┌");
            PrintHorizontalLine(120);
            Console.Write("┐");
            Console.WriteLine();

            Console.Write("│");
            Console.Write("  ");
            Console.Write(title);
            PrintHorizontalSpace(118 - title.Length);
            Console.Write("│");
            Console.WriteLine();

            Console.Write("├");
            PrintHorizontalLine(120);
            Console.Write("┤");
            Console.WriteLine();

            Console.Write("│");
            Console.Write("  ");
            string time = "Current Time: " + DateTime.Now.ToString();
            Console.Write(time);
            PrintHorizontalSpace(118 - time.Length);
            Console.Write("│");
            Console.WriteLine();

            Console.Write("├");
            for(int i = 0; i < contentColNumber - 1; i++)
            {
                PrintHorizontalLine(39);
                Console.Write("┬");
            }
            PrintHorizontalLine(40);
            Console.Write("┤");
            Console.WriteLine();
        }

        private static void PrintTopLine(int colWidth, int colNumber)
        {
            Console.Write("┌");
            for(int i = 0; i < colNumber - 1; i++)
            {
                PrintHorizontalLine(colWidth);
                Console.Write("┬");
            }
            PrintHorizontalLine(colWidth);
            Console.Write("┐");
            Console.WriteLine();
            Console.WriteLine("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
        }

        private static void PrintHorizontalLine(int width)
        {
            for(int i = 0; i < width; i++)
            {
                Console.Write("─");
            }
        }

        private static void PrintHorizontalSpace(int width)
        {
            for (int i = 0; i < width; i++)
            {
                Console.Write(" ");
            }
        }

    }
}
