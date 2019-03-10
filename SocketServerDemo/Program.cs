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
using System.Threading;

namespace SocketServerDemo
{
    delegate void SocketCreator(Socket socket);
    class Program
    {

        static HeartBeatWrapper HEART_BEAT_WRAPPER;

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            Logger.Init();
            InitHeartBeatData();
            SocketServer();
        }

        private static void InitHeartBeatData()
        {
            HEART_BEAT_WRAPPER = new HeartBeatWrapper().Action(ActionDefine.ACTION_HEART_BEAT);
        }

        public static void SocketServer()
        {
            int port = 2692;
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(100);
            Thread listenThread = new Thread(begin);
            listenThread.Start();
            Thread cleanThread = new Thread(clean);
            cleanThread.Start();
            Logger.ShowSimpleMessage("Server started.", "Server started at Port:" + port + ".");
            while (true)
            {
                string order = Console.ReadLine();
                if ("users".Equals(order))
                {
                    Logger.ShowCurrentUsers();
                }
                else if ("exit".Equals(order))
                {
                    ClientManager.Clear();
                    Environment.Exit(0);
                }
                else if ("clear".Equals(order))
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Unexpected Order!");
                }
            }
            
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

        public static void clean()
        {
            while (true)
            {
                Thread.Sleep(20 * 1000);
                try
                {
                    int count = 0;              
                    foreach(var client in ClientManager.GetAllClients())
                    {
                        if (client != null && client.User != null && client.ConnectionTimeout())
                        {
                            User user = client.User;
                            ClientManager.RemoveClient(client.UserCode);
                            NotifyUserChanged(user, false);
                            count++;
                        }
                    }
                    Logger.ShowSimpleMessage("Clean Thread is Working.", "[" + count + "] clients have been cleaned.");
                }
                catch(Exception ex)
                {
                    Logger.ShowSimpleMessage("Exception Catched.", ex.Message.Trim());
                    continue;
                }
            }
        }

        public static void NewSocket(Socket newSocket)
        {
            int exceptionTimes = 0;
            string userCode = null;

            while (true)
            {
                
                try
                {
                    if (userCode != null)
                    {
                        if (newSocket != null)
                        {
                            if (!ClientManager.ContainsClient(userCode))
                            {
                                newSocket.Close();
                                break;
                            }
                            Client client = ClientManager.GetClient(userCode);
                            if (client != null && client.ConnectionTimeout())
                            {
                                newSocket.Close();
                                ClientManager.RemoveClient(userCode);
                                NotifyUserChanged(client.User, false);
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    byte[] by = new byte[1024];
                    int length = 0;
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
                            HeartBeatWrapper heartBeatWrapper = new HeartBeatWrapper(content);
                            userCode = heartBeatWrapper.GetWrapperBody().UserCode;
                            HandleHeartBeat(newSocket, heartBeatWrapper);
                        }
                        else if(ActionDefine.ACTION_MESSAGE_SEND.Equals(action))
                        {
                            HandleMessageReceived(newSocket, new MessageWrapper(content));
                        }
                        else if (ActionDefine.ACTION_USER_CHANGED.Equals(action))
                        {
                            HandleUserChanged(newSocket, new UserWrapper(content));
                        }
                    }
                    exceptionTimes = 0;
                }
                catch(Exception ex)
                {
                    Logger.ShowSimpleMessage("Exception Catched.", ex.Message.Trim());
                    exceptionTimes++;
                    if(exceptionTimes == 3)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }     
                }
            }
        }

        private static void HandleHeartBeat(Socket socket, HeartBeatWrapper wrapper)
        {
            HeartBeatBody body = wrapper.GetWrapperBody();
            if (!ClientManager.ContainsClient(body.UserCode))
            {
                Client client = new Client();
                client.UserCode = body.UserCode;
                client.HeartBeatTime = DateTime.Now;
                client.Socket = socket;
                ClientManager.AddClient(client);
            }
            else
            {
                ClientManager.RefreshHeartBeatTime(body.UserCode);
            }
            HEART_BEAT_WRAPPER.SetBody(body);
            string reply = HEART_BEAT_WRAPPER.GetStringMessage();
            socket.Send(Encoding.UTF8.GetBytes(reply));
        }

        private static void HandleMessageReceived(Socket socket, MessageWrapper wrapper)
        {
            //消息分发
            MessageBody body = wrapper.GetWrapperBody();
            User user = body.User;
            Logger.ShowMessageReceived(user, body.Content);
            MessageWrapper broadcastWrapper = new MessageWrapper().Action(ActionDefine.ACTION_MESSAGE_SEND);
            MessageBody broadcastBody = body;
            broadcastWrapper.SetBody(broadcastBody);
            ClientManager.DistributeMessage(user.UserCode, broadcastWrapper.GetStringMessage());
            ClientManager.RefreshHeartBeatTime(user.UserCode);
        }

        private static void HandleUserChanged(Socket socket, UserWrapper wrapper)
        {
            UserBody body = wrapper.GetWrapperBody();
            User user = body.ChangedUser;
            Client client = ClientManager.GetClient(user.UserCode);
            if (client == null)
            {
                return;
            }
            else if(client.User == null)
            {
                ClientManager.SetUser(user.UserCode, user);
            }
            if (body.isUserLogon())
            {
                //响应加入群聊
                UserWrapper replyWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
                UserBody replyBody = new UserBody();
                replyBody.ChangedUser = body.ChangedUser;
                replyBody.CurrentUsers = ClientManager.GetAllUser();
                replyBody.Event = EventDefine.EVENT_USER_LOGON_SUCCESS;
                replyWrapper.SetBody(replyBody);
                string reply = replyWrapper.GetStringMessage();
                socket.Send(Encoding.UTF8.GetBytes(reply));
                //向其他用户广播
                NotifyUserChanged(body.ChangedUser, true);
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
                NotifyUserChanged(body.ChangedUser, false);
            }
        }

        private static void NotifyUserChanged(User user, bool add)
        {
            UserWrapper broadcastWrapper = new UserWrapper().Action(ActionDefine.ACTION_USER_CHANGED);
            UserBody broadcastBody = new UserBody();
            broadcastBody.ChangedUser = user;
            broadcastBody.Event = add ? EventDefine.EVENT_USER_IN : EventDefine.EVENT_USER_OUT;
            broadcastWrapper.SetBody(broadcastBody);
            ClientManager.DistributeMessage(user.UserCode, broadcastWrapper.GetStringMessage());
            if (add)
            {
                Logger.ShowUserAdded(user);
            }
            else
            {
                Logger.ShowUserQuited(user);
            }
            Logger.ShowCurrentUsers();
        }

    }
}
