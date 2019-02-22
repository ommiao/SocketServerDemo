using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServerDemo
{
    class Program
    {
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static byte[] result = new byte[1024];

        static void Main(string[] args)
        {
            SocketServer();
        }

        public static void SocketServer()
        {
            System.Console.WriteLine("Server started.");
            int port = 2692;
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(100);
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            System.Console.ReadLine();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientScoket = socket.Accept();
                Thread receivedThread = new Thread(ReceiveMessage);
                receivedThread.Start(clientScoket);
            }
        }

        private static void ReceiveMessage(object clientSocket)
        {
            Socket client = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    int receiveNumber = client.Receive(result);
                    if(receiveNumber == 0)
                    {
                        return;
                    }
                    string receive, reply;
                    receive = Encoding.UTF8.GetString(result, 0, receiveNumber);
                    if (receive.Equals("@heartbeat"))
                    {
                        reply = "@heartbeat@end\r\n";
                        Console.WriteLine(receive);
                    }
                    else
                    {
                        reply = "消息已接收到@end\r\n";
                        Console.WriteLine("接收到客户端{0}消息：{1}", client.RemoteEndPoint.ToString(), receive);
                    }
                    byte[] bs = Encoding.UTF8.GetBytes(reply);
                    client.Send(bs, bs.Length, 0);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    break;
                }
            }
        }
    }
}
