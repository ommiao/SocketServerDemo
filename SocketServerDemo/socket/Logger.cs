using System;

namespace SocketServerDemo.socket
{
    public class Logger
    {
        public static void Init()
        {
            Console.SetWindowSize(130, 32);
        }

        public static void ShowSimpleMessage(string message)
        {
            ShowSimpleMessage("Simple Message.", message);
        }

        public static void ShowSimpleMessage(string title, string message)
        {
            PrintCommonTitle(title);

            Console.Write("├");
            PrintHorizontalLine(122);
            Console.Write("┤");
            Console.WriteLine();

            Console.Write("│");
            Console.Write("  ");
            Console.Write(message);
            PrintHorizontalSpace(120 - message.Length);
            Console.Write("│");
            Console.WriteLine();

            Console.Write("└");
            PrintHorizontalLine(122);
            Console.Write("┘");
            Console.WriteLine();

        }

        public static void ShowCurrentUser()
        {
            PrintMessageTitle("This is a test title.", 3);
        }

        private static void PrintMessageTitle(string title, int contentColNumber)
        {

            PrintCommonTitle(title);

            Console.Write("├");
            int widthPerGrid = 40 * 3 / contentColNumber;
            for(int i = 0; i < contentColNumber - 1; i++)
            {
                PrintHorizontalLine(widthPerGrid);
                Console.Write("┬");
            }
            PrintHorizontalLine(widthPerGrid);
            Console.Write("┤");
            Console.WriteLine();
        }

        private static void PrintCommonTitle(string title)
        {
            Console.Write("┌");
            PrintHorizontalLine(122);
            Console.Write("┐");
            Console.WriteLine();

            Console.Write("│");
            Console.Write("  ");
            Console.Write(title);
            PrintHorizontalSpace(120 - title.Length);
            Console.Write("│");
            Console.WriteLine();

            Console.Write("├");
            PrintHorizontalLine(122);
            Console.Write("┤");
            Console.WriteLine();

            Console.Write("│");
            Console.Write("  ");
            string time = "Current Time: " + DateTime.Now.ToString();
            Console.Write(time);
            PrintHorizontalSpace(120 - time.Length);
            Console.Write("│");
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
