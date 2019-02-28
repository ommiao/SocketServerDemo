using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    abstract class MessageWrapper<M, T> where T : WrapperBody where M : MessageWrapper<M, T>
    {
        private MessageBase message = new MessageBase();

        public MessageWrapper()
        {

        }

        public MessageWrapper(T body)
        {

        }
    }
}
