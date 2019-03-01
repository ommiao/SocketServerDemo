using SocketServerDemo.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    class MessageBase : JavaBean
    {
        public string MsgNo { get; set; }

        public string Action { get; set; }

        public string Body { get; set; }

        public string ReplyTo { get; set; }

    }
}
