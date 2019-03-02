using SocketServerDemo.socket.message.user;
using System;
using System.Net.Sockets;

namespace SocketServerDemo.socket.service
{
    class Client
    {

        public Socket Socket { get; set; }

        public User User { get; set; }

        public DateTime HeartBeatTime { get; set; }

    }
}
