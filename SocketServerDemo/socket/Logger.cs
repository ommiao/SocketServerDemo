using SocketServerDemo.socket.message.user;
using SocketServerDemo.socket.service;
using SocketServerDemo.utils;
using System;
using System.Collections.Generic;

namespace SocketServerDemo.socket
{
    public class Logger
    {

        //private const string LEFT_TOP      = "┌";
        //private const string TOP           = "┬";
        //private const string RIGHT_TOP     = "┐";
        //private const string LEFT          = "├";
        //private const string CENTER        = "┼";
        //private const string RIGHT         = "┤";
        //private const string LEFT_BOTTOM   = "└";
        //private const string BOTTOM        = "┴";
        //private const string RIGHT_BOTTOM  = "┘";
        //private const string LINE_H        = "─";
        //private const string LINE_V        = "│";

        private const string LEFT_TOP = "|";
        private const string TOP = "-";
        private const string RIGHT_TOP = "|";
        private const string LEFT = "|";
        private const string CENTER = "|";
        private const string RIGHT = "|";
        private const string LEFT_BOTTOM = "|";
        private const string BOTTOM = "-";
        private const string RIGHT_BOTTOM = "|";
        private const string LINE_H = "-";
        private const string LINE_V = "|";

        private const int LINE_MAX = 122;
        private const int LINE_CONTENT_MAX = 118;
        private const int COLUMN_LENGTH = 40;
        private const int COLUMN_CONTENT_MAX = 36;
        private const int COLLUMN_CONTENT_INDENT = 2;

        public static void Init()
        {
            Console.SetWindowSize(130, 32);
        }

        public static void ShowMessageReceived(User user, string message)
        {
            PrintMessageTitle("Message Received.", 3);
            PrintTitleLine(new string[] { "UserCode", "Nickname", "Message" });
            int lines = StringUtil.GetLengthContainsCn(message) / COLUMN_CONTENT_MAX + 1;
            int middleLine = (lines - 1) / 2;
            for(int i = 0; i < lines; i++)
            {
                int lengthI = StringUtil.GetLengthContainsCn(message) > COLUMN_CONTENT_MAX ? COLUMN_CONTENT_MAX : StringUtil.GetLengthContainsCn(message);
                string msgI = StringUtil.Substring(message, lengthI);
                message = message.Remove(0, msgI.Length);
                if (i == middleLine)
                {
                    Console.Write(LINE_V);
                    PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                    Console.Write(user.UserCode);
                    int userCodeLength = user.UserCode.Length;
                    PrintHorizontalSpace(COLUMN_CONTENT_MAX - userCodeLength);
                    PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                    Console.Write(LINE_V);
                    PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                    Console.Write(user.Nickname);
                    int nicknameLength = user.Nickname.Length;
                    PrintHorizontalSpace(COLUMN_CONTENT_MAX - nicknameLength);
                    PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                }
                else
                {
                    Console.Write(LINE_V);
                    PrintHorizontalSpace(COLUMN_LENGTH);
                    Console.Write(LINE_V);
                    PrintHorizontalSpace(COLUMN_LENGTH);
                }
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                Console.Write(msgI);
                int length = StringUtil.GetLengthContainsCn(msgI);
                PrintHorizontalSpace(COLUMN_CONTENT_MAX - length);
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                Console.Write(LINE_V);
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

            Console.Write(LEFT);
            PrintHorizontalLine(LINE_MAX);
            Console.Write(RIGHT);
            Console.WriteLine();
            while(message.Length > 0)
            {
                int lengthI = StringUtil.GetLengthContainsCn(message) > LINE_CONTENT_MAX ? LINE_CONTENT_MAX : StringUtil.GetLengthContainsCn(message);
                string messageI = StringUtil.Substring(message, lengthI);
                message = message.Remove(0, messageI.Length);
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                Console.Write(messageI);
                PrintHorizontalSpace(LINE_CONTENT_MAX - StringUtil.GetLengthContainsCn(messageI));
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                Console.Write(LINE_V);
                Console.WriteLine();
            }
            PrintCommonBottom();
        }

        private static void PrintCommonBottom()
        {
            Console.Write(LEFT_BOTTOM);
            PrintHorizontalLine(LINE_MAX);
            Console.Write(RIGHT_BOTTOM);
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
            string type = add ? "User Added." : "User Quited.";
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(user.UserCode);
            int userCodeLength = user.UserCode.Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - userCodeLength);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(user.Nickname);
            int nicknameLength = user.Nickname.Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - nicknameLength);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write("User Added.");
            int length = "User Added.".Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - length);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            Console.WriteLine();
            PrintMessageBottom();
        }

        private static void PrintMessageBottom()
        {
            Console.Write(LEFT_BOTTOM);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(BOTTOM);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(BOTTOM);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(RIGHT_BOTTOM);
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void PrintTitleLine(string[] titles)
        {
            int length = titles.Length > 3 ? 3 : titles.Length;
            int spaces = 3 - length;
            for(int i = 0; i < length; i++)
            {
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
                Console.Write(titles[i]);
                PrintHorizontalSpace(COLUMN_CONTENT_MAX - titles[i].Length);
                PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            }
            for (int i = 0; i < spaces; i++)
            {
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLUMN_LENGTH);
            }
            Console.Write(LINE_V);
            Console.WriteLine();

            Console.Write(LEFT);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(CENTER);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(CENTER);
            PrintHorizontalLine(COLUMN_LENGTH);
            Console.Write(RIGHT);
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
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLUMN_LENGTH);
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLUMN_LENGTH);
                Console.Write(LINE_V);
                PrintHorizontalSpace(COLUMN_LENGTH);
                Console.Write(LINE_V);
                Console.WriteLine();
                PrintMessageBottom();
            }
        }

        private static void PrintUserLine(Client client, bool isLastLine)
        {
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(client.User.UserCode);
            int userCodeLength = client.User.UserCode.Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - userCodeLength);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(client.User.Nickname);
            int nicknameLength = client.User.Nickname.Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - nicknameLength);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(client.HeartBeatTime.ToString());
            int heartBeatTimeLength = client.HeartBeatTime.ToString().Length;
            PrintHorizontalSpace(COLUMN_CONTENT_MAX - heartBeatTimeLength);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            Console.WriteLine();
            if (isLastLine)
            {
                PrintMessageBottom();
            }
            else
            {
                Console.Write(LEFT);
                PrintHorizontalLine(COLUMN_LENGTH);
                Console.Write(CENTER);
                PrintHorizontalLine(COLUMN_LENGTH);
                Console.Write(CENTER);
                PrintHorizontalLine(COLUMN_LENGTH);
                Console.Write(RIGHT);
                Console.WriteLine();
            }
        }

        private static void PrintMessageTitle(string title, int contentColNumber)
        {

            PrintCommonTitle(title);

            Console.Write(LEFT);
            int widthPerGrid = COLUMN_LENGTH * 3 / contentColNumber;
            for(int i = 0; i < contentColNumber - 1; i++)
            {
                PrintHorizontalLine(widthPerGrid);
                Console.Write(TOP);
            }
            PrintHorizontalLine(widthPerGrid);
            Console.Write(RIGHT);
            Console.WriteLine();
        }

        private static void PrintCommonTitle(string title)
        {
            Console.Write(LEFT_TOP);
            PrintHorizontalLine(LINE_MAX);
            Console.Write(RIGHT_TOP);
            Console.WriteLine();

            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(title);
            PrintHorizontalSpace(LINE_CONTENT_MAX - title.Length);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            Console.WriteLine();

            Console.Write(LEFT);
            PrintHorizontalLine(LINE_MAX);
            Console.Write(RIGHT);
            Console.WriteLine();

            Console.Write(LINE_V);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            string time = "Current Time: " + DateTime.Now.ToString();
            Console.Write(time);
            PrintHorizontalSpace(LINE_CONTENT_MAX - time.Length);
            PrintHorizontalSpace(COLLUMN_CONTENT_INDENT);
            Console.Write(LINE_V);
            Console.WriteLine();
        }

        private static void PrintTopLine(int colWidth, int colNumber)
        {
            Console.Write(LEFT_TOP);
            for(int i = 0; i < colNumber - 1; i++)
            {
                PrintHorizontalLine(colWidth);
                Console.Write(TOP);
            }
            PrintHorizontalLine(colWidth);
            Console.Write(RIGHT_TOP);
            Console.WriteLine();
        }

        private static void PrintHorizontalLine(int width)
        {
            for(int i = 0; i < width; i++)
            {
                Console.Write(LINE_H);
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
