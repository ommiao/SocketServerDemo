using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using SocketServerDemo.socket.message;

namespace SocketServerDemo
{
    delegate void SocketCreator(Socket socket);
    class Program
    {

        static HeartBeatWrapper HEART_BEAT_WRAPPER;

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            InitHeartBeatData();
            SocketServer();
        }

        private static void InitHeartBeatData()
        {
            HEART_BEAT_WRAPPER = new HeartBeatWrapper().Action(SocketServerDemo.socket.Action.ACTION_HEART_BEAT);
        }

        public static void SocketServer()
        {
            int port = 2692;
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(100);
            System.Threading.Thread thread = new System.Threading.Thread(begin);
            thread.Start();
            Console.WriteLine("Server started.");
            Console.ReadLine();
        }

        public static void begin()
        {
            while (true)
            {
                Socket newSocket = socket.Accept();
                SocketCreator creator = new SocketCreator(NewSocket);
                creator.BeginInvoke(newSocket, null, null);
            }
        }

        public static void NewSocket(Socket newSocket)
        {
            while (true)
            {
                try
                {
                    byte[] by = new byte[1024];
                    int length = 0;
                    // newSocket.ReceiveTimeout = 50000;
                    //读取字符串
                    length = newSocket.Receive(by);
                    string content = Encoding.UTF8.GetString(by, 0, length);
                    while (length == 1024)
                    {
                        length = newSocket.Receive(by);
                        content += Encoding.UTF8.GetString(by, 0, length);
                    }

                    string reply;

                    if (content.Length >= 1)
                    {
                        ActionWrapper wrapper = new ActionWrapper(content);
                        if (SocketServerDemo.socket.Action.ACTION_HEART_BEAT.Equals(wrapper.GetAction()))
                        {
                            Console.WriteLine("@heartbeat: " + content);
                            reply = HEART_BEAT_WRAPPER.GetStringMessage();
                        }
                        else
                        {
                            Console.WriteLine("Message from Client: " + content);
                            reply = "From Server: Message Received.";
                        }
                        newSocket.Send(Encoding.UTF8.GetBytes(reply));

                    }
                    //文本读取完成
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    break;
                }
            }
        }

    }
}
