using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message.chat
{
    class MessageBody : WrapperBody
    {
        public string Content { get; set; }

        public string Nickname { get; set; }
    }
}
