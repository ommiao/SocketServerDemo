using SocketServerDemo.socket.message.user;
using SocketServerDemo.socket.service;
using SocketServerDemo.utils;
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

        public static void ShowMessageReceived(User user, string message)
        {
            PrintMessageTitle("Message Received.", 3);
            PrintTitleLine(new string[] { "UserCode", "Nickname", "Message" });
            int lines = StringUtil.GetLengthContainsCn(message) / 36 + 1;
            int middleLine = (lines - 1) / 2;
            for(int i = 0; i < lines; i++)
            {
                int lengthI = StringUtil.GetLengthContainsCn(message) > 36 ? 36 : StringUtil.GetLengthContainsCn(message);
                string msgI = StringUtil.Substring(message, lengthI);
                message = message.Remove(0, msgI.Length);
                if (i == middleLine)
                {
                    Console.Write("│  ");
                    Console.Write(user.UserCode);
                    int userCodeLength = user.UserCode.Length;
                    PrintHorizontalSpace(38 - userCodeLength);
                    Console.Write("│  ");
                    Console.Write(user.Nickname);
                    int nicknameLength = user.Nickname.Length;
                    PrintHorizontalSpace(38 - nicknameLength);
                }
                else
                {
                    Console.Write("│");
                    PrintHorizontalSpace(40);
                    Console.Write("│");
                    PrintHorizontalSpace(40);
                }
                Console.Write("│  ");
                Console.Write(msgI);
                int length = StringUtil.GetLengthContainsCn(msgI);
                PrintHorizontalSpace(38 - length);
                Console.Write("│");
                Console.WriteLine();
            }
            PrintMessageBottom();
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

            PrintCommonBottom();

        }

        private static void PrintCommonBottom()
        {
            Console.Write("└");
            PrintHorizontalLine(122);
            Console.Write("┘");
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ShowUserAdded(User user)
        {
            ShowUserChanged(user, true);
        }

        public static void ShowUserQuited(User user)
        {
            ShowUserChanged(user, false);
        }

        private static void ShowUserChanged(User user, bool add)
        {
            PrintMessageTitle("User Changed.", 3);
            PrintTitleLine(new string[]{ "UserCode", "Nickname", "Changed Type" });
            if (add)
            {
                Console.Write("│  ");
                Console.Write(user.UserCode);
                int userCodeLength = user.UserCode.Length;
                PrintHorizontalSpace(38 - userCodeLength);
                Console.Write("│  ");
                Console.Write(user.Nickname);
                int nicknameLength = user.Nickname.Length;
                PrintHorizontalSpace(38 - nicknameLength);
                Console.Write("│  ");
                Console.Write("User Added.");
                int length = "User Added.".Length;
                PrintHorizontalSpace(38 - length);
                Console.Write("│");
                Console.WriteLine();
            }
            else
            {
                Console.Write("│  ");
                Console.Write(user.UserCode);
                int userCodeLength = user.UserCode.Length;
                PrintHorizontalSpace(38 - userCodeLength);
                Console.Write("│  ");
                Console.Write(user.Nickname);
                int nicknameLength = user.Nickname.Length;
                PrintHorizontalSpace(38 - nicknameLength);
                Console.Write("│  ");
                Console.Write("User Quited.");
                int length = "User Quited.".Length;
                PrintHorizontalSpace(38 - length);
                Console.Write("│");
                Console.WriteLine();
            }
            PrintMessageBottom();
        }

        private static void PrintMessageBottom()
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

        private static void PrintTitleLine(string[] titles)
        {
            int length = titles.Length > 3 ? 3 : titles.Length;
            int spaces = 3 - length;
            for(int i = 0; i < length; i++)
            {
                Console.Write("│");
                Console.Write("  ");
                Console.Write(titles[i]);
                PrintHorizontalSpace(38 - titles[i].Length);
            }
            for (int i = 0; i < spaces; i++)
            {
                Console.Write("│");
                PrintHorizontalSpace(40);
            }
            Console.Write("│");
            Console.WriteLine();

            Console.Write("├");
            PrintHorizontalLine(40);
            Console.Write("┼");
            PrintHorizontalLine(40);
            Console.Write("┼");
            PrintHorizontalLine(40);
            Console.Write("┤");
            Console.WriteLine();
        }

        public static void ShowCurrentUsers()
        {
            List<Client> clients = ClientManager.GetAllClients();
            int count = clients.Count;
            PrintMessageTitle("Current Users. Number:" + count + ".", 3);
            PrintTitleLine(new string[] { "UserCode", "Nickname", "Heartbeat Time" });
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
                Console.Write("│");
                PrintHorizontalSpace(40);
                Console.Write("│");
                PrintHorizontalSpace(40);
                Console.Write("│");
                PrintHorizontalSpace(40);
                Console.Write("│");
                Console.WriteLine();
                PrintMessageBottom();
            }
        }

        private static void PrintUserLine(Client client, bool isLastLine)
        {
            Console.Write("│  ");
            Console.Write(client.User.UserCode);
            int userCodeLength = client.User.UserCode.Length;
            PrintHorizontalSpace(38 - userCodeLength);
            Console.Write("│  ");
            Console.Write(client.User.Nickname);
            int nicknameLength = client.User.Nickname.Length;
            PrintHorizontalSpace(38 - nicknameLength);
            Console.Write("│  ");
            Console.Write(client.HeartBeatTime.ToString());
            int heartBeatTimeLength = client.HeartBeatTime.ToString().Length;
            PrintHorizontalSpace(38 - heartBeatTimeLength);
            Console.Write("│");
            Console.WriteLine();
            if (isLastLine)
            {
                PrintMessageBottom();
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
