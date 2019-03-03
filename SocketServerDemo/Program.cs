using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using SocketServerDemo.socket.message;
using SocketServerDemo.socket.message.chat;
using SocketServerDemo.socket;
using SocketServerDemo.socket.message.user;
using SocketServerDemo.socket.service;
using SocketServerDemo.socket.message.heartbeat;

namespace SocketServerDemo
{
    delegate void SocketCreator(Socket socket);
    class Program
    {

        static HeartBeatWrapper HEART_BEAT_WRAPPER;
        static MessageWrapper MESSAGE_WRAPPER;

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            InitHeartBeatData();
            SocketServer();
        }

        private static void InitHeartBeatData()
        {
            HEART_BEAT_WRAPPER = new HeartBeatWrapper().Action(ActionDefine.ACTION_HEART_BEAT);
            MESSAGE_WRAPPER = new MessageWrapper().Action(ActionDefine.ACTION_MESSAGE_SEND);
            MessageBody body = new MessageBody();
            body.Content = "Message From Server.";
            User user = new User();
            user.Nickname = "Server";
            user.UserCode = "Server";
            body.User = user;
            MESSAGE_WRAPPER.SetBody(body);
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

                    if (content.Length >= 1)
                    {
                        ActionWrapper wrapper = new ActionWrapper(content);
                        string action = wrapper.GetAction();
                        if (ActionDefine.ACTION_HEART_BEAT.Equals(action))
                        {
                            Console.WriteLine("@heartbeat: " + content);
                            HandleHeartBeat(newSocket, new HeartBeatWrapper(content));
                        }
                        else if(ActionDefine.ACTION_MESSAGE_SEND.Equals(action))
                        {
                            Console.WriteLine("Message from Client: " + content);
                            HandleMessageReceived(newSocket, new MessageWrapper(content));

                        }
                        else if (ActionDefine.ACTION_USER_CHANGED.Equals(action))
                        {
                            Console.WriteLine("User Changed: " + content);
                            HandleUserChanged(newSocket, new UserWrapper(content));
                        }
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

        private static void HandleHeartBeat(Socket socket, HeartBeatWrapper wrapper)
        {
            HeartBeatBody body = wrapper.GetWrapperBody();
            ClientManager.RefreshHeartBeatTime(body.UserCode);
            HEART_BEAT_WRAPPER.SetBody(body);
            string reply = HEART_BEAT_WRAPPER.GetStringMessage();
            socket.Send(Encoding.UTF8.GetBytes(reply));
        }

        private static void HandleMessageReceived(Socket socket, MessageWrapper wrapper)
        {
            MessageBody body = wrapper.GetWrapperBody();

            User user = body.User;

            //string reply = MESSAGE_WRAPPER.GetStringMessage();
            //socket.Send(Encoding.UTF8.GetBytes(reply));

            MessageWrapper broadcastWrapper = new MessageWrapper().Action(ActionDefine.ACTION_MESSAGE_SEND);
            MessageBody broadcastBody = body;
            broadcastWrapper.SetBody(broadcastBody);
            ClientManager.DistributeMessage(user.UserCode, broadcastWrapper.GetStringMessage());

            Console.WriteLine("Reply to Client. UserCode is {0}, Nickname is {1}.", user.UserCode, user.Nickname);
        }

        private static void HandleUserChanged(Socket socket, UserWrapper wrapper)
        {
            UserBody body = wrapper.GetWrapperBody();
            User user = body.ChangedUser;
            if (body.isUserLogon())
            {
                //响应加入群聊
                Client client = new Client();
                client.Socket = socket;
                client.User = user;
                client.HeartBeatTime = DateTime.Now;
                ClientManager.AddClient(client);
                UserWrapper replyWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
                UserBody replyBody = new UserBody();
                replyBody.ChangedUser = body.ChangedUser;
                replyBody.CurrentUsers = ClientManager.GetAllUser();
                replyBody.Event = EventDefine.EVENT_USER_LOGON_SUCCESS;
                replyWrapper.SetBody(replyBody);
                string reply = replyWrapper.GetStringMessage();
                socket.Send(Encoding.UTF8.GetBytes(reply));

                //向其他用户广播
                UserWrapper broadcastWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
                UserBody broadcastBody = new UserBody();
                broadcastBody.ChangedUser = body.ChangedUser;
                broadcastBody.Event = EventDefine.EVENT_USER_IN;
                broadcastWrapper.SetBody(broadcastBody);
                ClientManager.DistributeMessage(user.UserCode, broadcastWrapper.GetStringMessage());

                Console.WriteLine("User Added. UserCode is {0}, Nickname is {1}.", user.UserCode, user.Nickname);
            }
            else if (body.isUserLogout())
            {
                //响应退出群聊
                ClientManager.RemoveClient(user.UserCode);
                UserWrapper replyWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
                UserBody replyBody = new UserBody();
                replyBody.ChangedUser = body.ChangedUser;
                replyBody.Event = EventDefine.EVENT_USER_LOGOUT_SUCCESS;
                replyWrapper.SetBody(replyBody);
                string reply = replyWrapper.GetStringMessage();
                socket.Send(Encoding.UTF8.GetBytes(reply));

                //向其他用户广播
                UserWrapper broadcastWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
                UserBody broadcastBody = new UserBody();
                broadcastBody.ChangedUser = body.ChangedUser;
                broadcastBody.Event = EventDefine.EVENT_USER_OUT;
                broadcastWrapper.SetBody(broadcastBody);
                ClientManager.DistributeMessage(user.UserCode, broadcastWrapper.GetStringMessage());

                Console.WriteLine("User Quited. UserCode is {0}, Nickname is {1}.", user.UserCode, user.Nickname);
            }
        }

    }
}
