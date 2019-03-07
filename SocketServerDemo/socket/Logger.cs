using SocketServerDemo.socket.service;
using System;
using System.Collections.Generic;

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
            Console.WriteLine();

        }

        public static void ShowCurrentUser()
        {
            List<Client> clients = ClientManager.GetAllClients();
            int count = clients.Count;
            PrintMessageTitle("Current Users.", 3);
            if (count > 0)
            {
                for(int i = 0; i < count - 1; i++)
                {
                    PrintUserLine(clients[i], false);
                }
                PrintUserLine(clients[count - 1], true);
            }
            else
            {
                Console.Write("│  No User.");
                PrintHorizontalSpace(40 - "  No User.".Length);
                Console.Write("│");
                PrintHorizontalSpace(40);
                Console.Write("│");
                PrintHorizontalSpace(40);
                Console.Write("│");
                Console.WriteLine();
                Console.Write("└");
                PrintHorizontalLine(40);
                Console.Write("┴");
                PrintHorizontalLine(40);
                Console.Write("┴");
                PrintHorizontalLine(40);
                Console.Write("┘");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void PrintUserLine(Client client, bool isLastLine)
        {
            Console.Write("│  ");
            Console.Write(client.User.UserCode);
            int userCodeLength = client.User.UserCode.Length;
            for(int i = 0; i < 38 - userCodeLength; i++)
            {
                Console.Write(" ");
            }
            Console.Write("│  ");
            Console.Write(client.User.Nickname);
            int nicknameLength = client.User.Nickname.Length;
            for(int i = 0; i < 38 - nicknameLength; i++)
            {
                Console.Write(" ");
            }
            Console.Write("│  ");
            Console.Write(client.HeartBeatTime.ToString());
            int heartBeatTimeLength = client.HeartBeatTime.ToString().Length;
            for (int i = 0; i < 38 - heartBeatTimeLength; i++)
            {
                Console.Write(" ");
            }
            Console.Write("│");
            Console.WriteLine();
            if (isLastLine)
            {
                Console.Write("└");
                PrintHorizontalLine(40);
                Console.Write("┴");
                PrintHorizontalLine(40);
                Console.Write("┴");
                PrintHorizontalLine(40);
                Console.Write("┘");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.Write("├");
                PrintHorizontalLine(40);
                Console.Write("┼");
                PrintHorizontalLine(40);
                Console.Write("┼");
                PrintHorizontalLine(40);
                Console.Write("┤");
                Console.WriteLine();
            }
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
