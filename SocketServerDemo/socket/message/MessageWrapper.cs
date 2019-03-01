using SocketServerDemo.entity;
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
            message.Body = body.toJson();
        }

        public MessageWrapper(String message)
        {
            this.message = MessageBase.fromJson<MessageBase>(message);
        }

        public string GetStringMessage()
        {
            return message.toJson();
        }

        public T GetWrapperBody()
        {
            return JavaBean.fromJson<T>(message.Body);
        }

        public M Action(String action)
        {
            message.Action = action;
            return (M)this;
        }

        public string GetAction()
        {
            return message.Action;
        }

    }
}
