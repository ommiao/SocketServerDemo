using SocketServerDemo.socket.message.user;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SocketServerDemo.socket.service
{
    class ClientManager
    {
        private static object lockObj = new object();

        private static Dictionary<string, Client> allClients = new Dictionary<string, Client>();

        public static Client GetClient(string userCode)
        {
            return allClients[userCode];
        }

        public static void AddClient(Client client)
        {
            lock (lockObj)
            {
                allClients.Add(client.UserCode, client);
            }
        }

        public static void SetUser(string userCode, User user)
        {
            if (!ContainsClient(userCode))
            {
                return;
            }
            lock (lockObj)
            {
                allClients[userCode].User = user;
            }
        }

        public static void RemoveClient(string userCode)
        {
            lock (lockObj)
            {
                if (ContainsClient(userCode))
                {
                    allClients.Remove(userCode);
                }
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
            lock (lockObj)
            {
                Client client = allClients[userCode];
                client.HeartBeatTime = System.DateTime.Now;
            }
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

        public static void DistributeMessage(string message)
        {
            DistributeMessage(null, message);
        }

        public static void DistributeMessage(string excludeUserCode, string message)
        {
            lock (lockObj)
            {
                foreach (var userCode in allClients.Keys)
                {
                    if (userCode.Equals(excludeUserCode))
                    {
                        continue;
                    }
                    else if (allClients[userCode].ConnectionTimeout())
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
}
