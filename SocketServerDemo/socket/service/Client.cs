using SocketServerDemo.socket.message.user;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SocketServerDemo.socket.service
{
    class Client
    {

        public string UserCode { get; set; }

        public User User { get; set; }

        public Socket Socket { get; set; }

        public DateTime HeartBeatTime { get; set; }

        public bool DiedFlag { get; set; }

        public bool ConnectionTimeout()
        {
            TimeSpan interval = DateTime.Now - HeartBeatTime;
            return interval.TotalSeconds > 30;
        }

    }
}
