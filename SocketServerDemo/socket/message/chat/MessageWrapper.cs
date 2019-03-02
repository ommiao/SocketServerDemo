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

        public MessageWrapper() : base()
        {

        }

        public MessageWrapper(string message) : base(message)
        {

        }

        public void Content(MessageBody body)
        {
            SetBody(body);
        }

    }
}
