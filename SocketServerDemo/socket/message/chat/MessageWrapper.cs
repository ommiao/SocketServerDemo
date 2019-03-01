using SocketServerDemo.socket.message.chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    class MessageWrapper : AbstractMessageWrapper<MessageWrapper, MessageBody>
    {

        public void Content(MessageBody body)
        {
            SetBody(body);
        }

    }
}
