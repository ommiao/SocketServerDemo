using SocketServerDemo.socket.message.user;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SocketServerDemo.socket.service
{
    class ClientManager
    {
        private static Dictionary<string, Client> allClients = new Dictionary<string, Client>();

        public static void AddClient(Client client)
        {
            RemoveClient(client.User.UserCode);
            allClients.Add(client.User.UserCode, client);
        }

        public static void RemoveClient(string userCode)
        {
            if (ContainsClient(userCode))
            {
                if (allClients[userCode].Socket != null)
                {
                    allClients[userCode].Socket.Close();
                }
                allClients.Remove(userCode);
            }
        }

        public static bool ContainsClient(string userCode)
        {
            return allClients.ContainsKey(userCode);
        }

        public static void RefreshHeartBeatTime(string userCode)
        {
            if (!ContainsClient(userCode))
            {
                return;
            }
            Client client = allClients[userCode];
            client.HeartBeatTime = System.DateTime.Now;
        }

        public static List<User> GetAllUser()
        {
            List<User> users = new List<User>();
            foreach(var client in allClients.Values)
            {
                users.Add(client.User);
            }
            return users;
        }

        private static void RemoveDiedClient()
        {
            foreach(var userCode in allClients.Keys)
            {
                Socket socket = allClients[userCode].Socket;
                if (socket == null || !socket.Connected)
                {
                    RemoveClient(userCode);
                }
            }
        }

        public static void DistributeMessage(string message)
        {
            DistributeMessage(null, message);
        }

        public static void DistributeMessage(string excludeUserCode, string message)
        {
            RemoveDiedClient();
            foreach(var userCode in allClients.Keys)
            {
                if (userCode.Equals(excludeUserCode))
                {
                    continue;
                }
                else
                {
                    allClients[userCode].Socket.Send(Encoding.UTF8.GetBytes(message));
                }
            }
        }

    }
}
