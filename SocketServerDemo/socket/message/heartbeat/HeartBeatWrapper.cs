using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerDemo.socket.message
{
    class HeartBeatWrapper : AbstractMessageWrapper<HeartBeatWrapper, WrapperBody>
    {
        public HeartBeatWrapper()
        {
        }

        public HeartBeatWrapper(string message) : base(message)
        {
        }

    }
}
