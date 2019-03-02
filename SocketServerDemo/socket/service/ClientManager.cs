using System.Collections.Generic;

namespace SocketServerDemo.socket.service
{
    class ClientManager
    {
        private static Dictionary<string, Client> allClients = new Dictionary<string, Client>();

        public static void AddClient(Client client)
        {
            allClients.Add(client.User.UserCode, client);
        }

        public static void RemoveClient(string userCode)
        {
            allClients.Remove(userCode);
        }

    }
}
