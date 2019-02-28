using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    class MessageBase
    {
        private string msgNo { get; set; }

        private string action { get; set; }

        private string body { get; set; }

        private string replyTo { get; set; }

    }
}
